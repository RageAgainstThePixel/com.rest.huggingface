using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Conversational
{
    public sealed class ConversationalTask : BaseJsonPayloadInferenceTask
    {
        public ConversationalTask(Conversation input, ConversationalParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("microsoft/DialoGPT-large"), options)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public Conversation Input { get; }

        [JsonProperty("parameters")]
        public ConversationalParameters Parameters { get; }

        public override string TaskId => "conversational";
    }
}
