using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationTaskResult : InferenceTaskResult
    {
        public SummarizationTaskResult(string content, JsonSerializerSettings jsonSerializerSettings)
            : base(content, jsonSerializerSettings)
        {
            Summaries = JsonConvert.DeserializeObject<IReadOnlyList<SummarizationResponse>>(content, jsonSerializerSettings);
        }

        public IReadOnlyList<SummarizationResponse> Summaries { get; }
    }
}
