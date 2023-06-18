using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ImageClassification
{
    public sealed class ImageClassificationResult
    {
        [JsonConstructor]
        public ImageClassificationResult(
            [JsonProperty("score")] double score,
            [JsonProperty("label")] string label
        )
        {
            Score = score;
            Label = label;
        }

        [JsonProperty("score")]
        public double Score { get; }

        [JsonProperty("label")]
        public string Label { get; }
    }
}
