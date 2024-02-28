// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationParameters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doSample">Activate logits sampling.</param>
        /// <param name="maxTokens">Maximum number of generated tokens.</param>
        /// <param name="repetitionPenalty">The parameter for repetition penalty. 1.0 means no penalty.</param>
        /// <param name="returnFullText">Whether to prepend the prompt to the generated text.</param>
        /// <param name="stopSequences">Stop generating tokens if a member of 'stop_sequences' is generated.</param>
        /// <param name="seed">Random sampling seed.</param>
        /// <param name="temperature">The value used to module the logits distribution.</param>
        /// <param name="topK">The number of highest probability vocabulary tokens to keep for top-k-filtering.</param>
        /// <param name="topP">If set to &lt; 1, only the smallest set of most probable tokens with probabilities that add up to 'top_p' or higher are kept for generation.</param>
        /// <param name="truncate">Truncate inputs tokens to the given size.</param>
        /// <param name="typicalP">Typical Decoding mass.</param>
        /// <param name="bestOf">Generate 'best_of' sequences and return the one if the highest token logprobs.</param>
        /// <param name="watermark">Enable watermarking.</param>
        /// <param name="details">Get generation details.</param>
        /// <param name="decoderInputDetails">Get decoder input token logprobs and ids.</param>
        public TextGenerationParameters(
            [JsonProperty("do_sample")] bool? doSample = null,
            [JsonProperty("max_new_tokens")] int? maxTokens = null,
            [JsonProperty("repetition_penalty")] float? repetitionPenalty = null,
            [JsonProperty("return_full_text")] bool? returnFullText = null,
            [JsonProperty("stop")] OneOrMoreOf<string> stopSequences = null,
            [JsonProperty("seed")] uint? seed = null,
            [JsonProperty("temperature")] float? temperature = null,
            [JsonProperty("top_k")] uint? topK = null,
            [JsonProperty("top_p")] float? topP = null,
            [JsonProperty("truncate")] uint? truncate = null,
            [JsonProperty("typical_p")] float? typicalP = null,
            [JsonProperty("best_of")] uint? bestOf = null,
            [JsonProperty("watermark")] bool? watermark = null,
            [JsonProperty("details")] bool? details = null,
            [JsonProperty("decoder_input_details")] bool? decoderInputDetails = null)
        {
            if (bestOf.HasValue)
            {
                if (bestOf is not > 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(bestOf)} must be > 0!");
                }

                if (seed is > 1)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(seed)} must not be set if {nameof(bestOf)} is > 1");
                }

                var sampling = doSample.HasValue && (temperature.HasValue || topK.HasValue || topP.HasValue || typicalP.HasValue);

                if (!sampling)
                {
                    throw new ArgumentException($"You must use sampling if {nameof(bestOf)} is > 1");
                }
            }

            DoSample = doSample;
            MaxTokens = maxTokens;

            if (repetitionPenalty is <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(repetitionPenalty)} must be > 0!");
            }

            RepetitionPenalty = repetitionPenalty;
            ReturnFullText = returnFullText;
            StopSequences = stopSequences?.Values;

            if (seed is <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(seed)} must be > 0!");
            }

            Seed = seed;

            if (temperature is <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(temperature)} must be > 0!");
            }

            Temperature = temperature;

            if (topK is <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(topK)} must be > 0!");
            }

            TopK = topK;

            if (topP is < 0f or > 1f)
            {
                throw new ArgumentOutOfRangeException($"{nameof(topP)} must be between 0 and 1!");
            }

            TopP = topP;

            if (truncate <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(truncate)} must be > 0!");
            }

            Truncate = truncate;

            if (typicalP is < 0f or > 1f)
            {
                throw new ArgumentOutOfRangeException($"{nameof(typicalP)} must be between 0 and 1!");
            }

            TypicalP = typicalP;
            BestOf = bestOf;
            Watermark = watermark;
            Details = details;
            DecoderInputDetails = decoderInputDetails;
        }

        /// <summary>
        /// (Optional: True). Bool. Whether or not to use sampling, use greedy decoding otherwise.
        /// </summary>
        [JsonProperty("do_sample")]
        public bool? DoSample { get; }

        /// <summary>
        /// (Default: None). Int (0-512). The amount of new tokens to be generated,
        /// this does not include the input length it is a estimate of the size of generated text you want.
        /// Each new tokens slows down the request, so look for balance between response times and length of text generated.
        /// </summary>
        [JsonProperty("max_new_tokens")]
        public int? MaxTokens { get; }

        /// <summary>
        /// (Default: None). Float (0.0-100.0).
        /// The more a token is used within generation the more it is penalized to not be picked in successive generation passes.
        /// </summary>
        [JsonProperty("repetition_penalty")]
        public float? RepetitionPenalty { get; }

        /// <summary>
        /// (Default: True). Bool. If set to False, the return results will not contain the original query making it easier for prompting.
        /// </summary>
        [JsonProperty("return_full_text")]
        public bool? ReturnFullText { get; }

        /// <summary>
        /// (Default: None). Stop generating tokens if a member of stop_sequences is generated.
        /// </summary>
        [JsonProperty("stop")]
        public IReadOnlyList<string> StopSequences { get; }

        /// <summary>
        /// (Default: None). Random sampling seed.
        /// </summary>
        [JsonProperty("seed")]
        public uint? Seed { get; }

        /// <summary>
        /// (Default: None). Float (0-120.0). The amount of time in seconds that the query should take maximum.
        /// Network can cause some overhead so it will be a soft limit. Use that in combination with max_new_tokens for best results.
        /// </summary>
        [JsonProperty("max_time")]
        public float? MaxTime { get; }

        /// <summary>
        /// (Default: 1). Integer. The number of proposition you want to be returned.
        /// </summary>
        [JsonProperty("num_return_sequences")]
        public int? NumberOfResults { get; }

        /// <summary>
        /// (Default: 1.0). Float (0.0-100.0). The temperature of the sampling operation.
        /// 1 means regular sampling, 0 means always take the highest score, 100.0 is getting closer to uniform probability.
        /// </summary>
        [JsonProperty("temperature")]
        public float? Temperature { get; }

        /// <summary>
        /// (Default: None). Integer to define the top tokens considered within the sample operation to create new text.
        /// </summary>
        [JsonProperty("top_k")]
        public uint? TopK { get; }

        /// <summary>
        /// (Default: None). Float to define the tokens that are within the sample operation of text generation.
        /// Add tokens in the sample for more probable to least probable until the sum of the probabilities is greater than top_p.
        /// </summary>
        [JsonProperty("top_p")]
        public float? TopP { get; }

        /// <summary>
        /// Truncate inputs tokens to the given size.
        /// </summary>
        [JsonProperty("truncate")]
        public uint? Truncate { get; }

        /// <summary>
        /// Typical Decoding mass.
        /// See Typical Decoding for Natural Language Generation <see href="https://arxiv.org/abs/2202.00666"/> for more information.
        /// </summary>
        [JsonProperty("typical_p")]
        public float? TypicalP { get; }

        /// <summary>
        /// (Default: None). Integer to define the top tokens considered within the sample operation to create new text.
        /// </summary>
        [JsonProperty("best_of")]
        public uint? BestOf { get; }

        /// <summary>
        ///  Watermarking with A Watermark for Large Language Models <see href="https://arxiv.org/abs/2301.10226"/>.
        /// </summary>
        [JsonProperty("watermark")]
        public bool? Watermark { get; }

        /// <summary>
        /// Get generation details.
        /// </summary>
        [JsonProperty("details")]
        public bool? Details { get; }

        /// <summary>
        /// Get decoder input token logprobs and ids.
        /// </summary>
        [JsonProperty("decoder_input_details")]
        public bool? DecoderInputDetails { get; }
    }
}
