using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationResponse : InferenceTaskResponse
    {
        public SummarizationResponse(string content, JsonSerializerSettings jsonSerializerSettings)
            : base(content, jsonSerializerSettings)
        {
            Summaries = JsonConvert.DeserializeObject<IReadOnlyList<SummarizationResult>>(content, jsonSerializerSettings);
        }

        public IReadOnlyList<SummarizationResult> Summaries { get; }
    }
}
