using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.ZeroShotClassification
{
    public sealed class ZeroShotClassificationResult
    {
        [JsonConstructor]
        public ZeroShotClassificationResult(
            [JsonProperty("sequence")] string sequence,
            [JsonProperty("labels")] List<string> labels,
            [JsonProperty("scores")] List<double> scores)
        {
            Sequence = sequence;
            Labels = labels;
            Scores = scores;
        }

        [JsonProperty("sequence")]
        public string Sequence { get; }

        [JsonProperty("labels")]
        public IReadOnlyList<string> Labels { get; }

        [JsonProperty("scores")]
        public IReadOnlyList<double> Scores { get; }
    }
}
