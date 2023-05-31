using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering
{
    public sealed class QuestionAnsweringTaskResult : InferenceTaskResult
    {
        public QuestionAnsweringTaskResult(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Result = JsonConvert.DeserializeObject<QuestionAnsweringResponse>(content, settings);
        }

        public QuestionAnsweringResponse Result { get; }
    }
}
