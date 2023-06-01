using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationTask : BaseJsonPayloadInferenceTask
    {
        public override string TaskId => "summarization";

        public SummarizationTask(string input, SummarizationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("facebook/bart-large-cnn"), options)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        [JsonProperty("parameters")]
        public SummarizationParameters Parameters { get; }
    }
}
