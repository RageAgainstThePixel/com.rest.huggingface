using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Translation
{
    public sealed class TranslationResult
    {
        public TranslationResult([JsonProperty("translation_text")] string text)
        {
            Text = text;
        }

        [JsonProperty("translation_text")]
        public string Text { get; }
    }
}
