using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.SentenceSimilarity
{
    public sealed class SentenceSimilarityResponse : InferenceTaskResponse
    {
        public SentenceSimilarityResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Scores = JsonConvert.DeserializeObject<IReadOnlyList<double>>(content, settings);
        }

        public IReadOnlyList<double> Scores { get; }
    }
}
