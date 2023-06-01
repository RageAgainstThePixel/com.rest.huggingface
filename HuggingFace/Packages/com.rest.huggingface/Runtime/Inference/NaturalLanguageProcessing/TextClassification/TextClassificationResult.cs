using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextClassification
{
    public class TextClassificationResult
    {
        [JsonConstructor]
        public TextClassificationResult(
            [JsonProperty("label")] string label,
            [JsonProperty("score")] double score)
        {
            Label = label;
            Score = score;
        }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("score")]
        public double Score { get; }
    }
}
