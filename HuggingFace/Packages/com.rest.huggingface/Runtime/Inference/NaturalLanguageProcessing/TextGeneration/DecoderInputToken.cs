using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    [Preserve]
    public sealed class DecoderInputToken
    {
        [JsonConstructor]
        public DecoderInputToken(
            [JsonProperty("id")] int id,
            [JsonProperty("text")] string text,
            [JsonProperty("logprob")] float? logProb)
        {
            Id = id;
            Text = text;
            LogProb = logProb;
        }

        /// <summary>
        /// Token ID from the model tokenizer.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; }

        /// <summary>
        /// Token text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; }

        /// <summary>
        /// Optional, since the logprob of the first token cannot be computed.
        /// </summary>
        [JsonProperty("logprob")]
        public float? LogProb { get; }
    }
}
