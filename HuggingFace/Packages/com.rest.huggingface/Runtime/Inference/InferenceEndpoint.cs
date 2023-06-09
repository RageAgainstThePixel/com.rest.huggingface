// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.Async;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference
{
    public sealed class InferenceEndpoint : HuggingFaceBaseEndpoint
    {
        public bool EnableLogging { get; set; } = true;

        public int MaxRetryAttempts { get; set; } = 3;

        public int Timeout { get; set; } = -1;

        public InferenceEndpoint(HuggingFaceClient client) : base(client) { }

        protected override string Root => "models";

        public async Task<TResponse> RunInferenceTaskAsync<TTask, TResponse>(TTask task, CancellationToken cancellationToken = default)
            where TResponse : InferenceTaskResponse
            where TTask : InferenceTask
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            task.Model ??= await client.HubEndpoint.GetRecommendedModelAsync<TTask>(cancellationToken);

            if (string.IsNullOrWhiteSpace(task.Model?.ModelId))
            {
                throw new InvalidOperationException($"no valid model to run {task.Id}");
            }

            var endpoint = GetInferenceUrl(task.Model.ModelId);
            Response response;
            var attempt = 0;

            async Task<Response> CallEndpointAsync()
            {
                try
                {
                    var jsonData = await task.ToJsonAsync(client.JsonSerializationOptions, cancellationToken).ConfigureAwait(true);

                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        if (EnableLogging)
                        {
                            Debug.Log(jsonData);
                        }

                        response = await Rest.PostAsync(endpoint, jsonData, parameters: new RestParameters(client.DefaultRequestHeaders, timeout: Timeout), cancellationToken);
                    }
                    else
                    {
                        var byteData = await task.ToByteArrayAsync(cancellationToken);
                        // TODO ensure proper accept headers are set here
                        response = await Rest.PostAsync(endpoint, byteData, parameters: new RestParameters(client.DefaultRequestHeaders, timeout: Timeout), cancellationToken);
                    }

                    response.Validate(EnableLogging);
                }
                catch (RestException restEx)
                {
                    if (restEx.Response.Code == 503 &&
                        task.Options.WaitForModel)
                    {
                        if (EnableLogging)
                        {
                            Debug.LogWarning($"Waiting for model, attempt {attempt} of {MaxRetryAttempts}\n{restEx}");
                        }

                        if (++attempt == MaxRetryAttempts)
                        {
                            throw;
                        }

                        HuggingFaceError error;

                        try
                        {
                            error = JsonConvert.DeserializeObject<HuggingFaceError>(restEx.Response.Body);
                        }
                        catch (JsonSerializationException jsonEx)
                        {
                            Debug.LogError($"Failed to parse error response: \"restEx.Response.Error\"\n{jsonEx.Message}");
                            throw restEx;
                        }

                        await Task.Delay(TimeSpan.FromSeconds(error.EstimatedTime), cancellationToken);
                        response = await CallEndpointAsync();
                    }
                    else
                    {
                        throw;
                    }
                }

                return response;
            }

            response = await CallEndpointAsync();

            if (typeof(JsonInferenceTaskResponse).IsAssignableFrom(typeof(TResponse)))
            {
                var jsonResponse = Activator.CreateInstance(typeof(TResponse), response.Body, client.JsonSerializationOptions) as TResponse;

                if (jsonResponse is B64JsonInferenceTaskResponse b64JsonInferenceTaskResponse)
                {
                    await b64JsonInferenceTaskResponse.DecodeAsync(cancellationToken);
                }

                return jsonResponse;
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

            throw new InvalidOperationException($"{typeof(TResponse).Name} does not implement a known {nameof(InferenceTaskResponse)}!");
        }
    }
}
