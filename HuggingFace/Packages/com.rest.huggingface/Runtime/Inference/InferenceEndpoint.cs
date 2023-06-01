using System;
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
    }
}
