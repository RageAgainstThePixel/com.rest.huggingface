using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationResult
    {
        [JsonConstructor]
        public SummarizationResult([JsonProperty("summary_text")] string text)
        {
            Text = text;
        }

        [JsonProperty("summary_text")]
        public string Text { get; }
    }
}
