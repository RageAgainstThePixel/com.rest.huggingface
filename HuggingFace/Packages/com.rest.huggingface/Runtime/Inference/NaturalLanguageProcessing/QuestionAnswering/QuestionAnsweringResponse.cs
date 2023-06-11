using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering
{
    public sealed class QuestionAnsweringResponse : JsonInferenceTaskResponse
    {
        public QuestionAnsweringResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Result = JsonConvert.DeserializeObject<QuestionAnsweringResult>(content, settings);
        }

        public QuestionAnsweringResult Result { get; }
    }
}
