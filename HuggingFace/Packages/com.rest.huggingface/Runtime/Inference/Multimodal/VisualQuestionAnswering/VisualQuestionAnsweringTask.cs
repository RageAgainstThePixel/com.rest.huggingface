// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Multimodal.VisualQuestionAnswering
{
    public sealed class VisualQuestionAnsweringTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public VisualQuestionAnsweringTask() { }

        public VisualQuestionAnsweringTask(VisualQuestionAnsweringInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        public VisualQuestionAnsweringInput Input { get; }

        public override string Id => "visual-question-answering";

        public override Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
            => Input.ToJson(settings, cancellationToken);
    }
}
