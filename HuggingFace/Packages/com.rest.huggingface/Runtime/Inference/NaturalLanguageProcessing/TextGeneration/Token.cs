using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    [Preserve]
    public sealed class Token
    {
        [Preserve]
        [JsonConstructor]
        public Token(
            [JsonProperty("id")] int id,
            [JsonProperty("text")] string text,
            [JsonProperty("logprobs")] float logProbs,
            [JsonProperty("special")] bool special)
        {
            Id = id;
            Text = text;
            LogProbs = logProbs;
            Special = special;
        }

        /// <summary>
        /// Token ID from the model tokenizer.
        /// </summary>
        [Preserve]
        [JsonProperty("id")]
        public int Id { get; }

        /// <summary>
        /// Token text
        /// </summary>
        [Preserve]
        [JsonProperty("text")]
        public string Text { get; }

        /// <summary>
        /// Logprob.
        /// </summary>
        [Preserve]
        [JsonProperty("logprobs")]
        public float LogProbs { get; }

        /// <summary>
        /// Is the token a special token?
        /// Can be used to ignore tokens when concatenating.
        /// </summary>
        [Preserve]
        [JsonProperty("special")]
        public bool Special { get; }
    }
}
