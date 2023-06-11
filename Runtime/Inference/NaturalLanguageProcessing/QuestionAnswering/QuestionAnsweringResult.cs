using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering
{
    public class QuestionAnsweringResult
    {
        [JsonConstructor]
        public QuestionAnsweringResult(
            [JsonProperty("score")] double score,
            [JsonProperty("start")] int start,
            [JsonProperty("end")] int end,
            [JsonProperty("answer")] string answer
        )
        {
            Score = score;
            Start = start;
            End = end;
            Answer = answer;
        }

        [JsonProperty("score")]
        public double Score { get; }

        [JsonProperty("start")]
        public int Start { get; }

        [JsonProperty("end")]
        public int End { get; }

        [JsonProperty("answer")]
        public string Answer { get; }
    }
}
