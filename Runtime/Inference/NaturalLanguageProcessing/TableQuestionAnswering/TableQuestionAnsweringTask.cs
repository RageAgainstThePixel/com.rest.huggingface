using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering
{
    public sealed class TableQuestionAnsweringTask<TTableData> : BaseJsonPayloadInferenceTask
        where TTableData : class
    {
        public TableQuestionAnsweringTask(TableQuestionAnsweringInput<TTableData> input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("google/tapas-base-finetuned-wtq"), options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public TableQuestionAnsweringInput<TTableData> Input { get; }

        public override string Id => "table-question-answering";
    }
}
