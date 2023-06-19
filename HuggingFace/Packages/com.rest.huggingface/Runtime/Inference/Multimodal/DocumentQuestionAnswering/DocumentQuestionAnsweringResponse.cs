// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.DocumentQuestionAnswering
{
    public sealed class DocumentQuestionAnsweringResponse : BaseQuestionAnsweringResponse
    {
        public DocumentQuestionAnsweringResponse(string content, JsonSerializerSettings settings) : base(content, settings) { }
    }
}
