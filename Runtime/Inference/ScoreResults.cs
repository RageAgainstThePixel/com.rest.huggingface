// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference
{
    public class ScoreResults
    {
        [JsonConstructor]
        public ScoreResults(
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
