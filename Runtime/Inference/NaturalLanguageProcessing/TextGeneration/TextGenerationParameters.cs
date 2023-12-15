using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationParameters
    {
        public TextGenerationParameters(
            [JsonProperty("top_k")] float? topK = null,
            [JsonProperty("top_p")] float? topP = null,
            [JsonProperty("temperature")] float? temperature = null,
            [JsonProperty("repetition_penalty")] float? repetitionPenalty = null,
            [JsonProperty("max_new_tokens")] int? maxTokens = null,
            [JsonProperty("max_time")] float? maxTime = null,
            [JsonProperty("return_full_text")] bool? returnFullText = null,
            [JsonProperty("num_return_sequences")] int? numberOfResults = null,
            [JsonProperty("do_sample")] bool? doSample = null)
        {
            TopK = topK;
            TopP = topP;
            Temperature = temperature;
            RepetitionPenalty = repetitionPenalty;
            MaxTokens = maxTokens;
            MaxTime = maxTime;
            ReturnFullText = returnFullText;
            NumberOfResults = numberOfResults;
            DoSample = doSample;
        }

        /// <summary>
        /// (Default: None). Integer to define the top tokens considered within the sample operation to create new text.
        /// </summary>
        [JsonProperty("top_k")]
        public float? TopK { get; set; }

        /// <summary>
        /// (Default: None). Float to define the tokens that are within the sample operation of text generation.
        /// Add tokens in the sample for more probable to least probable until the sum of the probabilities is greater than top_p.
        /// </summary>
        [JsonProperty("top_p")]
        public float? TopP { get; set; }

        /// <summary>
        /// (Default: 1.0). Float (0.0-100.0). The temperature of the sampling operation.
        /// 1 means regular sampling, 0 means always take the highest score, 100.0 is getting closer to uniform probability.
        /// </summary>
        [JsonProperty("temperature")]
        public float? Temperature { get; set; }

        /// <summary>
        /// (Default: None). Float (0.0-100.0). The more a token is used within generation the more it is penalized to not be picked in successive generation passes.
        /// </summary>
        [JsonProperty("repetition_penalty")]
        public float? RepetitionPenalty { get; set; }

        /// <summary>
        /// (Default: None). Int (0-250). The amount of new tokens to be generated, this does not include the input length it is a estimate of the size of generated text you want. Each new tokens slows down the request, so look for balance between response times and length of text generated.
        /// </summary>
        [JsonProperty("max_new_tokens")]
        public int? MaxTokens { get; set; }

        /// <summary>
        /// (Default: None). Float (0-120.0). The amount of time in seconds that the query should take maximum.
        /// Network can cause some overhead so it will be a soft limit. Use that in combination with max_new_tokens for best results.
        /// </summary>
        [JsonProperty("max_time")]
        public float? MaxTime { get; set; }

        /// <summary>
        /// (Default: True). Bool. If set to False, the return results will not contain the original query making it easier for prompting.
        /// </summary>
        [JsonProperty("return_full_text")]
        public bool? ReturnFullText { get; set; }

        /// <summary>
        /// (Default: 1). Integer. The number of proposition you want to be returned.
        /// </summary>
        [JsonProperty("num_return_sequences")]
        public int? NumberOfResults { get; set; }

        /// <summary>
        /// (Optional: True). Bool. Whether or not to use sampling, use greedy decoding otherwise.
        /// </summary>
        [JsonProperty("do_sample")]
        public bool? DoSample { get; set; }
    }
}
