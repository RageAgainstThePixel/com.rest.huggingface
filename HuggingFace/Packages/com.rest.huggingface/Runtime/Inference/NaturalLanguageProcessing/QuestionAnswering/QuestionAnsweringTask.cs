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
}
