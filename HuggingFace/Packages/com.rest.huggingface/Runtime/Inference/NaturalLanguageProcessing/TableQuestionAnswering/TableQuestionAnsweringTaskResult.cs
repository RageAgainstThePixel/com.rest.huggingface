using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering
{
    public sealed class TableQuestionAnsweringTaskResult : InferenceTaskResult
    {
        public TableQuestionAnsweringTaskResult(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Result = JsonConvert.DeserializeObject<TableQuestionAnsweringResult>(content, settings);
        }

        public TableQuestionAnsweringResult Result { get; }
    }
}
