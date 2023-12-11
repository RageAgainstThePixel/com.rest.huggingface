// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationParameters
    {
        public TextGenerationParameters(
            [JsonProperty("best_of")] int? bestOf = null,
            [JsonProperty("do_sample")] bool? doSample = null,
            [JsonProperty("max_new_tokens")] int? maxTokens = null,
            [JsonProperty("repetition_penalty")] float? repetitionPenalty = null,
            [JsonProperty("return_full_text")] bool? returnFullText = null,
            [JsonProperty("seed")] int? seed = null,
            [JsonProperty("stop")] OneOrMoreOf<string> stopSequences = null,
            [JsonProperty("temperature")] float? temperature = null,
            [JsonProperty("top_k")] int? topK = null,
            [JsonProperty("top_p")] float? topP = null,
            [JsonProperty("typical_p")] float? typicalP = null,
            [JsonProperty("truncate")] uint? truncate = null,
            [JsonProperty("watermark")] bool? watermark = null
            )
        {
            if (bestOf is > 1)
            {
                if (seed is > 1)
                {
                    throw new ArgumentException($"{nameof(seed)} must not be set if {nameof(bestOf)} is > 1");
                }

                var sampling = doSample.HasValue && doSample.Value || temperature.HasValue || topK.HasValue || topP.HasValue || typicalP.HasValue;

                if (!sampling)
                {
                    throw new ArgumentException($"You must use sampling if {nameof(bestOf)} is > 1");
                }
            }

            BestOf = bestOf;
            DoSample = doSample;
            MaxTokens = maxTokens;
            RepetitionPenalty = repetitionPenalty;
            ReturnFullText = returnFullText;
            Seed = seed;
            StopSequences = stopSequences?.Values;
            Temperature = temperature;
            TopK = topK;
            TopP = topP;
            Truncate = truncate;
            TypicalP = typicalP;
            Watermark = watermark;
        }

        /// <summary>
        /// (Default: None). Integer to define the top tokens considered within the sample operation to create new text.
        /// </summary>
        [JsonProperty("best_of")]
        public int? BestOf { get; set; }

        /// <summary>
        /// (Optional: True). Bool. Whether or not to use sampling, use greedy decoding otherwise.
        /// </summary>
        [JsonProperty("do_sample")]
        public bool? DoSample { get; set; }

        /// <summary>
        /// (Default: None). Int (0-512). The amount of new tokens to be generated,
        /// this does not include the input length it is a estimate of the size of generated text you want.
        /// Each new tokens slows down the request, so look for balance between response times and length of text generated.
        /// </summary>
        [JsonProperty("max_new_tokens")]
        public int? MaxTokens { get; set; }

        /// <summary>
        /// (Default: None). Float (0.0-100.0).
        /// The more a token is used within generation the more it is penalized to not be picked in successive generation passes.
        /// </summary>
        [JsonProperty("repetition_penalty")]
        public float? RepetitionPenalty { get; set; }

        /// <summary>
        /// (Default: True). Bool. If set to False, the return results will not contain the original query making it easier for prompting.
        /// </summary>
        [JsonProperty("return_full_text")]
        public bool? ReturnFullText { get; set; }

        /// <summary>
        /// (Default: None). Random sampling seed.
        /// </summary>
        [JsonProperty("seed")]
        public int? Seed { get; set; }

        /// <summary>
        /// (Default: None). Stop generating tokens if a member of stop_sequences is generated.
        /// </summary>
        [JsonProperty("stop")]
        public IReadOnlyList<string> StopSequences { get; set; }

        /// <summary>
        /// (Default: None). Float (0-120.0). The amount of time in seconds that the query should take maximum.
        /// Network can cause some overhead so it will be a soft limit. Use that in combination with max_new_tokens for best results.
        /// </summary>
        [JsonProperty("max_time")]
        public float? MaxTime { get; set; }

        /// <summary>
        /// (Default: 1). Integer. The number of proposition you want to be returned.
        /// </summary>
        [JsonProperty("num_return_sequences")]
        public int? NumberOfResults { get; set; }

        /// <summary>
        /// (Default: 1.0). Float (0.0-100.0). The temperature of the sampling operation.
        /// 1 means regular sampling, 0 means always take the highest score, 100.0 is getting closer to uniform probability.
        /// </summary>
        [JsonProperty("temperature")]
        public float? Temperature { get; set; }

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
        public float? TopP { get; set; }

        /// <summary>
        /// Truncate inputs tokens to the given size.
        /// </summary>
        [JsonProperty("truncate")]
        public uint? Truncate { get; set; }

        /// <summary>
        /// Typical Decoding mass.
        /// See Typical Decoding for Natural Language Generation <see href="https://arxiv.org/abs/2202.00666"/> for more information.
        /// </summary>
        [JsonProperty("typical_p")]
        public float? TypicalP { get; set; }

        /// <summary>
        ///  Watermarking with A Watermark for Large Language Models <see href="https://arxiv.org/abs/2301.10226"/>.
        /// </summary>
        [JsonProperty("watermark")]
        public bool? Watermark { get; set; }
    }
}
