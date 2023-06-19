// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Multimodal.DocumentQuestionAnswering
{
    public sealed class DocumentQuestionAnsweringTask : BaseQuestionAnsweringTask
    {
        [Preserve]
        public DocumentQuestionAnsweringTask() { }

        public DocumentQuestionAnsweringTask(SingleSourceQuestionAnsweringInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "document-question-answering";
    }
}
