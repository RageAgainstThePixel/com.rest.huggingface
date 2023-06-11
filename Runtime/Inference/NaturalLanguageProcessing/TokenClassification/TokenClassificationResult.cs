using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification
{
    public sealed class TokenClassificationResult
    {
        [JsonConstructor]
        public TokenClassificationResult(
            [JsonProperty("entity_group")] string entityGroup,
            [JsonProperty("score")] double score,
            [JsonProperty("word")] string word,
            [JsonProperty("start")] int start,
            [JsonProperty("end")] int end
        )
        {
            EntityGroup = entityGroup;
            Score = score;
            Word = word;
            Start = start;
            End = end;
        }

        [JsonProperty("entity_group")]
        public string EntityGroup { get; }

        [JsonProperty("score")]
        public double Score { get; }

        [JsonProperty("word")]
        public string Word { get; }

        [JsonProperty("start")]
        public int Start { get; }

        [JsonProperty("end")]
        public int End { get; }
    }
}