using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering
{
    public sealed class TableQuestionAnsweringResponse : InferenceTaskResponse
    {
        public TableQuestionAnsweringResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Result = JsonConvert.DeserializeObject<TableQuestionAnsweringResult>(content, settings);
        }

        public TableQuestionAnsweringResult Result { get; }
    }
}
