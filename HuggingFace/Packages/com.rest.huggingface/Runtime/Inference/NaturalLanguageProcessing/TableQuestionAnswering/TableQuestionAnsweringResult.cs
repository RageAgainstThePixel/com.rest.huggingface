using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering
{
    public sealed class TableQuestionAnsweringResult
    {
        [JsonConstructor]
        public TableQuestionAnsweringResult(
            [JsonProperty("answer")] string answer,
            [JsonProperty("cells")] List<string> cells,
            [JsonProperty("aggregator")] string aggregator,
            [JsonProperty("coordinates")] List<List<int>> coordinates)
        {
            Answer = answer;
            Cells = cells;
            Aggregator = aggregator;
            Coordinates = coordinates;
        }

        [JsonProperty("answer")]
        public string Answer { get; }

        [JsonProperty("cells")]
        public IReadOnlyList<string> Cells { get; }

        [JsonProperty("aggregator")]
        public string Aggregator { get; }

        [JsonProperty("coordinates")]
        public IReadOnlyList<List<int>> Coordinates { get; }
    }
}
