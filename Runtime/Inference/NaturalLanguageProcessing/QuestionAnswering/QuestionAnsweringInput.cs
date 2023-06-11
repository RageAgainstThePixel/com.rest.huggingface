using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering
{
    public sealed class QuestionAnsweringInput
    {
        public QuestionAnsweringInput(string question, string context)
        {
            Question = question;
            Context = context;
        }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }
    }
}
