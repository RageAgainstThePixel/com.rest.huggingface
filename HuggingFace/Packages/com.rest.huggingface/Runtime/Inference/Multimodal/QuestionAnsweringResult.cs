// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal
{
    public sealed class QuestionAnsweringResult
    {
        [JsonConstructor]
        public QuestionAnsweringResult(
            [JsonProperty("score")] double score,
            [JsonProperty("answer")] string answer
        )
        {
            Score = score;
            Answer = answer;
        }

        [JsonProperty("score")]
        public double Score { get; }

        [JsonProperty("answer")]
        public string Answer { get; }
    }
}
