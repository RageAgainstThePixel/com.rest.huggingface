using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationResult
    {
        [Preserve]
        [JsonProperty("generated_text")]
        public string Text { get; set; }
    }
}
