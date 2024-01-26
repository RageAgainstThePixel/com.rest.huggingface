// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference
{
    [Preserve]
    public sealed class InferenceEndpoint : HuggingFaceBaseEndpoint
    {
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

            if (EnableDebug)
            {
                Debug.Log(endpoint);
            }

            Response response;
            var attempt = 0;

            async Task<Response> CallEndpointAsync()
            {
                try
                {
                    var headers = client.DefaultRequestHeaders.ToDictionary(pair => pair.Key, pair => pair.Value);

                    if (!string.IsNullOrWhiteSpace(task.MimeType))
                    {
                        headers.Add("Accept", task.MimeType);
                    }

                    var jsonData = await task.ToJsonAsync(client.JsonSerializationOptions, cancellationToken).ConfigureAwait(true);

                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        if (EnableDebug)
                        {
                            Debug.Log(jsonData);
                        }

                        response = await Rest.PostAsync(endpoint, jsonData, parameters: new RestParameters(headers, timeout: Timeout), cancellationToken);
                    }
                    else
                    {
                        var byteData = await task.ToByteArrayAsync(cancellationToken);
                        response = await Rest.PostAsync(endpoint, byteData, parameters: new RestParameters(headers, timeout: Timeout), cancellationToken);
                    }

                    response.Validate(EnableDebug);
                }
                catch (RestException restEx)
                {
                    if (restEx.Response.Code == 503 && task.Options.WaitForModel)
                    {
                        if (++attempt == MaxRetryAttempts) { throw; }

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

                        if (EnableDebug)
                        {
                            Debug.LogWarning($"Waiting for model for {error.EstimatedTime} seconds... attempt {attempt} of {MaxRetryAttempts}\n{restEx}");
                        }

                        await Task.Delay(TimeSpan.FromSeconds(error.EstimatedTime), cancellationToken).ConfigureAwait(true);
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
                    await b64JsonInferenceTaskResponse.DecodeAsync(EnableDebug, cancellationToken);
                }

                return jsonResponse;
            }

            if (typeof(BinaryInferenceTaskResponse).IsAssignableFrom(typeof(TResponse)))
            {
                var binaryResponse = Activator.CreateInstance(typeof(TResponse)) as TResponse;

                if (binaryResponse is BinaryInferenceTaskResponse taskResponse)
                {
                    if (response.Headers.TryGetValue("Content-Type", out var contentType))
                    {
                        Debug.Log($"{typeof(TResponse).Name} Content-Type: {contentType}");
                    }

                    await using var contentStream = new MemoryStream(response.Data);

                    try
                    {
                        await taskResponse.DecodeAsync(contentStream, EnableDebug, cancellationToken);
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
