using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationResponse
    {
        [JsonConstructor]
        public SummarizationResponse([JsonProperty("summary_text")] string text)
        {
            Text = text;
        }

        [JsonProperty("summary_text")]
        public string Text { get; }
    }
}
