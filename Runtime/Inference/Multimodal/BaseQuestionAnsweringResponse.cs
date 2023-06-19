// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal
{
    public abstract class BaseQuestionAnsweringResponse : JsonInferenceTaskResponse
    {
        protected BaseQuestionAnsweringResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<QuestionAnsweringResult>>(content, settings);
        }

        public IReadOnlyList<QuestionAnsweringResult> Results { get; }
    }
}
