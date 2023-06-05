using Newtonsoft.Json;

namespace HuggingFace.Inference.Audio
{
    public sealed class AudioClassificationResults
    {
        [JsonConstructor]
        public AudioClassificationResults(
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
