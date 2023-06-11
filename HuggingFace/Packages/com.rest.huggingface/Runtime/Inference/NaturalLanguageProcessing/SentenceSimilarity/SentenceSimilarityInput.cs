using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.SentenceSimilarity
{
    public sealed class SentenceSimilarityInput
    {
        public SentenceSimilarityInput(string source, List<string> sentences)
        {
            Source = source;
            Sentences = sentences;
        }

        [JsonProperty("source_sentence")]
        public string Source { get; }

        [JsonProperty("sentences")]
        public IReadOnlyList<string> Sentences { get; }
    }
}
