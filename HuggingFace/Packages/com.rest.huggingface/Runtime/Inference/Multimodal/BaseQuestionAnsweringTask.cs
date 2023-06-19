// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal
{
    public abstract class BaseQuestionAnsweringTask : BaseJsonPayloadInferenceTask
    {
        protected BaseQuestionAnsweringTask() { }

        protected BaseQuestionAnsweringTask(SingleSourceQuestionAnsweringInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        public SingleSourceQuestionAnsweringInput Input { get; }

        public override Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
            => Input.ToJson(settings, cancellationToken);
    }
}
