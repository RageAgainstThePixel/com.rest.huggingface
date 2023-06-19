// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HuggingFace.Inference
{
    public abstract class BaseJsonPayloadInferenceTask : InferenceTask
    {
        protected BaseJsonPayloadInferenceTask() { }

        protected BaseJsonPayloadInferenceTask(ModelInfo model, InferenceOptions options) : base(model, options) { }

        public override Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
            => Task.FromResult(JsonConvert.SerializeObject(this, settings));
    }
}
