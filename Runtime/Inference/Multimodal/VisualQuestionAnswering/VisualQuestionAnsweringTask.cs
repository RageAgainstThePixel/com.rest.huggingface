// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Multimodal.VisualQuestionAnswering
{
    public sealed class VisualQuestionAnsweringTask : BaseQuestionAnsweringTask
    {
        [Preserve]
        public VisualQuestionAnsweringTask() { }

        public VisualQuestionAnsweringTask(SingleSourceQuestionAnsweringInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options) { }

        public override string Id => "visual-question-answering";
    }
}
