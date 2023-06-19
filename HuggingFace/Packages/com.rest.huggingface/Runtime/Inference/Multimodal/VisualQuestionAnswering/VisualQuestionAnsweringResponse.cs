// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.VisualQuestionAnswering
{
    public sealed class VisualQuestionAnsweringResponse : BaseQuestionAnsweringResponse
    {
        public VisualQuestionAnsweringResponse(string content, JsonSerializerSettings settings) : base(content, settings) { }
    }
}
