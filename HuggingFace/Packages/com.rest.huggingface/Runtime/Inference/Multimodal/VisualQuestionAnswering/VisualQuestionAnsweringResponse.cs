// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.VisualQuestionAnswering
{
    public sealed class VisualQuestionAnsweringResponse : JsonInferenceTaskResponse
    {
        public VisualQuestionAnsweringResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<VisualQuestionAnsweringResult>>(content, settings);
        }

        public IReadOnlyList<VisualQuestionAnsweringResult> Results { get; }
    }
}
