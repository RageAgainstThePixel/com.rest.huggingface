using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Audio
{
    public sealed class TextToSpeechTask : BaseJsonPayloadInferenceTask
    {
        public TextToSpeechTask(string input, ModelInfo model, InferenceOptions options = null)
            : base(model ?? new ModelInfo("facebook/fastspeech2-en-ljspeech"), options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        public override string Id => "text-to-speech";
    }
}
