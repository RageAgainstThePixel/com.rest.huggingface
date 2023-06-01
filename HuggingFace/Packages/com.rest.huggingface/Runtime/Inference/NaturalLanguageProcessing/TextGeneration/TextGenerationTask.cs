using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationTask : BaseJsonPayloadInferenceTask
    {
        public TextGenerationTask(string input, TextGenerationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("gpt2"), options)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        [JsonProperty("parameters")]
        public TextGenerationParameters Parameters { get; }

        public override string TaskId => "text-generation";
    }
}
