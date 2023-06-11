using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification
{
    public sealed class TokenClassificationParameters
    {
        public enum AggregationStrategy
        {
            /// <summary>
            /// Every token gets classified without further aggregation.
            /// </summary>
            None,
            /// <summary>
            /// Entities are grouped according to the default schema (B-, I- tags get merged when the tag is similar).
            /// </summary>
            Simple,
            /// <summary>
            /// Same as the simple strategy except words cannot end up with different tags. Words will use the tag of the first token when there is ambiguity.
            /// </summary>
            First,
            /// <summary>
            /// Same as the simple strategy except words cannot end up with different tags. Scores are averaged across tokens and then the maximum label is applied.
            /// </summary>
            Average,
            /// <summary>
            /// 
            /// </summary>
            Max
        }

        public TokenClassificationParameters()
        {
            Strategy = AggregationStrategy.Simple;
        }

        public TokenClassificationParameters(AggregationStrategy strategy)
        {
            Strategy = strategy;
        }

        /// <summary>
        /// (Default: simple). There are several aggregation strategies:
        /// none: Every token gets classified without further aggregation.
        /// simple: Entities are grouped according to the default schema (B-, I- tags get merged when the tag is similar).
        /// first: Same as the simple strategy except words cannot end up with different tags. Words will use the tag of the first token when there is ambiguity.
        /// average: Same as the simple strategy except words cannot end up with different tags. Scores are averaged across tokens and then the maximum label is applied.
        /// max: Same as the simple strategy except words cannot end up with different tags. Word entity will be the token with the maximum score.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("aggregation_strategy", DefaultValueHandling = DefaultValueHandling.Include)]
        public AggregationStrategy Strategy { get; set; }
    }
}
