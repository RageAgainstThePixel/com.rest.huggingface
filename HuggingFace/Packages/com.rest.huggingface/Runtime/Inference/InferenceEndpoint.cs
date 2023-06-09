using HuggingFace.Hub;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference
{
    public sealed class InferenceEndpoint : HuggingFaceBaseEndpoint
    {
        public InferenceEndpoint(HuggingFaceClient client) : base(client) { }

        protected override string Root => "models";

        public async Task<TResponse> RunInferenceTaskAsync<TTask, TResponse>(TTask task, CancellationToken cancellationToken = default)
            where TResponse : InferenceTaskResponse
            where TTask : InferenceTask
        {
            var endpoint = GetInferenceUrl(task.Model);
            Response response;

            if (typeof(BaseJsonPayloadInferenceTask).IsAssignableFrom(typeof(TTask)))
            {
                var jsonData = task.ToJson(client.JsonSerializationOptions);
                Debug.Log(jsonData);
                response = await Rest.PostAsync(endpoint, jsonData, parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            }
            else if (typeof(BinaryInferenceTaskResponse).IsAssignableFrom(typeof(TTask)))
            {
                var byteData = await task.ToByteArrayAsync(cancellationToken);
                // DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("audio/wav"));
                response = await Rest.PostAsync(endpoint, byteData, parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            }
            else
            {
                throw new InvalidOperationException($"{nameof(TTask)} does not implement a known task!");
            }

            response.Validate(true);

            if (typeof(JsonInferenceTaskResponse).IsAssignableFrom(typeof(TResponse)))
            {
                return Activator.CreateInstance(typeof(TResponse), response.Body, client.JsonSerializationOptions) as TResponse;
            }

            if (typeof(BinaryInferenceTaskResponse).IsAssignableFrom(typeof(TResponse)))
            {
                var binaryResponse = Activator.CreateInstance(typeof(TResponse)) as TResponse;

                if (binaryResponse is BinaryInferenceTaskResponse taskResponse)
                {
                    await using var contentStream = new MemoryStream(response.Data);

                    try
                    {
                        await taskResponse.DecodeAsync(contentStream, cancellationToken);
                    }
                    finally
                    {
                        await contentStream.DisposeAsync();
                    }
                }

                return binaryResponse;
            }

            throw new InvalidOperationException($"{nameof(TResponse)} does not implement a known task responses!");
        }

        public async Task<IReadOnlyList<ModelInfo>> GetRecommendedModelsAsync(PipelineTag task, CancellationToken cancellationToken = default)
        {
            var models = await client.HubEndpoint.ListModelsAsync(
                new ModelSearchArguments(
                    new ModelFilter(task: task.ToString()),
                    sort: "downloads",
                    sortDirection: ModelSearchArguments.Direction.Descending,
                    limit: 5),
                cancellationToken);
            var results = new ConcurrentBag<ModelInfo>();
            var tasks = models.Select(GetModelDetailsTask).ToList();
            await Task.WhenAll(tasks);
            return results.ToList();

            #region locals

            Task GetModelDetailsTask(ModelInfo model)
            {
                async Task GetModelDetailsTaskInternalAsync()
                {
                    try
                    {
                        results.Add(await client.HubEndpoint.GetModelDetailsAsync(model.ModelId, cancellationToken: cancellationToken));
                    }
                    catch (Exception e)
                    {
                        if (e is RestException httpEx &&
                            httpEx.Response.Code == 403)
                        {
                            Debug.LogWarning(httpEx.Message);
                        }
                        else
                        {
                            Debug.LogError(e);
                        }
                    }
                }

                return GetModelDetailsTaskInternalAsync();
            }

            #endregion locals
        }
    }
}
