using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationInferenceTask : BaseJsonPayloadInferenceTask
    {
        public override string TaskId => "summarization";

        public SummarizationInferenceTask(string input, ModelInfo model = null, SummarizationParams parameters = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("facebook/bart-large-cnn"), options)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        [JsonProperty("parameters")]
        public SummarizationParams Parameters { get; }
    }
}
