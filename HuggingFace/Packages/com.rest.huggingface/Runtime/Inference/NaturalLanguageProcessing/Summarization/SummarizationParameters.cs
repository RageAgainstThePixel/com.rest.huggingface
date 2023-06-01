using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationParameters
    {
        public SummarizationParameters(
            [JsonProperty("min_length")] int? minLength = null,
            [JsonProperty("max_length")] int? maxLength = null,
            [JsonProperty("top_k")] int? topK = null,
            [JsonProperty("top_p")] int? topP = null,
            [JsonProperty("temperature")] float? temperature = null,
            [JsonProperty("repetition_penalty")] float? repetitionPenalty = null,
            [JsonProperty("max_time")] float? maxTime = null)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            TopK = topK;
            TopP = topP;
            Temperature = temperature;
            RepetitionPenalty = repetitionPenalty;
            MaxTime = maxTime;
        }

        /// <summary>
        /// (Default: None). Integer to define the minimum length in tokens of the output summary.
        /// </summary>
        [JsonProperty("min_length")]
        public int? MinLength { get; set; }

        /// <summary>
        /// (Default: None). Integer to define the maximum length in tokens of the output summary.
        /// </summary>
        [JsonProperty("max_length")]
        public int? MaxLength { get; set; }

        /// <summary>
        /// (Default: None). Integer to define the top tokens considered within the sample operation to create new text.
        /// </summary>
        [JsonProperty("top_k")]
        public int? TopK { get; set; }

        /// <summary>
        /// (Default: None). Float to define the tokens that are within the sample operation of text generation.
        /// Add tokens in the sample for more probable to least probable until the sum of the probabilities is greater than top_p.
        /// </summary>
        [JsonProperty("top_p")]
        public int? TopP { get; set; }

        /// <summary>
        /// (Default: 1.0). Float (0.0-100.0). The temperature of the sampling operation. 1 means regular sampling, 0 means always take the highest score, 100.0 is getting closer to uniform probability.
        /// </summary>
        [JsonProperty("temperature")]
        public float? Temperature { get; set; }

        /// <summary>
        /// (Default: None). Float (0.0-100.0). The more a token is used within generation the more it is penalized to not be picked in successive generation passes.
        /// </summary>
        [JsonProperty("repetition_penalty")]
        public float? RepetitionPenalty { get; set; }

        /// <summary>
        /// (Default: None). Float (0-120.0). The amount of time in seconds that the query should take maximum. Network can cause some overhead so it will be a soft limit.
        /// </summary>
        [JsonProperty("max_time")]
        public float? MaxTime { get; set; }
    }
}
