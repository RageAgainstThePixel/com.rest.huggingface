using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering
{
    public sealed class QuestionAnsweringTask : BaseJsonPayloadInferenceTask
    {
        public QuestionAnsweringTask(QuestionAnsweringInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("deepset/roberta-base-squad2"), options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public QuestionAnsweringInput Input { get; }

        public override string TaskId => "question-answering";
    }

    public sealed class TableQuestionAnsweringTask : BaseJsonPayloadInferenceTask
    {
        public TableQuestionAnsweringTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string TaskId => "table-question-answering";
    }
}
