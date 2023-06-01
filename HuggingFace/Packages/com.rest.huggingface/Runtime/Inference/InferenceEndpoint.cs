using HuggingFace.Hub;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.Rest.Extensions;

namespace HuggingFace.Inference
{
    public sealed class InferenceEndpoint : HuggingFaceBaseEndpoint
    {
        public InferenceEndpoint(HuggingFaceClient client) : base(client) { }

        protected override string Root => "models";

        public async Task<TResult> RunInferenceTaskAsync<TTask, TResult>(TTask task, CancellationToken cancellationToken = default)
            where TResult : InferenceTaskResponse
            where TTask : InferenceTask
        {
            var endpoint = GetInferenceUrl(task.Model.ModelId);
            Debug.Log(endpoint);

            var json = task.ToJson(client.JsonSerializationOptions);
            HttpContent payload;

            if (!string.IsNullOrWhiteSpace(json))
            {
                Debug.Log(json);
                payload = json.ToJsonStringContent();
            }
            else
            {
                payload = new ByteArrayContent(task.ToByteArray());
            }

            var result = await client.Client.PostAsync(endpoint, payload, cancellationToken);
            var resultAsString = await result.ReadAsStringAsync(true);
            var taskResult = Activator.CreateInstance(typeof(TResult), resultAsString, client.JsonSerializationOptions) as TResult;
            return taskResult;
        }

        public async Task<IReadOnlyList<ModelInfo>> GetRecommendedModelsAsync(PipelineTag task, CancellationToken cancellationToken = default)
        {
            var models = await client.HubEndpoint.ListModelsAsync(
                new ModelSearchArguments(
                    new ModelFilter(task: task.ToString()),
                    sort: "likes",
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
                        if (e is HttpRequestException httpEx &&
                            httpEx.Message.Contains("[403]"))
                        {
                            Debug.LogWarning(httpEx.Message);
                        }
                        else
                        {
                            Debug.LogError(e);
                        }
                    }
                }

                return Task.Run(GetModelDetailsTaskInternalAsync, cancellationToken);
            }

            #endregion locals
        }
    }
}
