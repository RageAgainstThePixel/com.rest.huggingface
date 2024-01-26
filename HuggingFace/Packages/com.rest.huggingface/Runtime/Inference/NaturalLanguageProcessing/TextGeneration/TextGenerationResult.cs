using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    [Preserve]
    public sealed class TextGenerationResult
    {
        [JsonConstructor]
        public TextGenerationResult(
            [JsonProperty("generated_text")] string text,
            [JsonProperty("details")] Details details)
        {
            Text = text;
            Details = details;
        }

        [Preserve]
        [JsonProperty("generated_text")]
        public string Text { get; }

        [Preserve]
        [JsonProperty("details")]
        public Details Details { get; }
    }

    [Preserve]
    public sealed class Details
    {
        [Preserve]
        [JsonConstructor]
        public Details(
            [JsonProperty("finish_reason")] string finishReason,
            [JsonProperty("generated_tokens")] uint generatedTokens,
            [JsonProperty("seed")] uint? seed = null)
        {
        }

        [Preserve]
        [JsonProperty("finish_reason")]
        public string FinishReason { get; }

        [Preserve]
        [JsonProperty("generated_tokens")]
        public uint GeneratedTokens { get; }

        [Preserve]
        [JsonProperty("seed")]
        public uint? Seed { get; }
    }

    public sealed class BestOfSequence
    {
        [JsonProperty("generated_text")]
        public string GeneratedText { get; }

        [JsonProperty("finish_reason")]
        public FinishReason FinishReason { get; }

        [JsonProperty("generated_tokens")]
        public int GeneratedTokens { get; }

        [JsonProperty("seed")]
        public int? Seed { get; }

        [JsonProperty("prefill")]
        public IReadOnlyList<DecoderInputToken> Prefill { get; }

        [JsonProperty("tokens")]
        public IReadOnlyList<Token> Tokens { get; }
    }

    [Preserve]
    public enum FinishReason
    {
        /// <summary>
        /// Number of generated tokens == 'max_new_tokens'.
        /// </summary>
        [EnumMember(Value = "length")]
        Length,
        /// <summary>
        /// The model generated its end of sequence token.
        /// </summary>
        [EnumMember(Value = "eos_token")]
        EndOfSequenceToken,
        /// <summary>
        /// The model generated a text included in 'stop_sequences'.
        /// </summary>
        [EnumMember(Value = "stop_sequence")]
        StopSequence,
    }
}
