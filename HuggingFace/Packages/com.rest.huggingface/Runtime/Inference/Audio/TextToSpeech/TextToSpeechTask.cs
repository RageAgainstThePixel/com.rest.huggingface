// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Audio
{
    public sealed class TextToSpeechTask : BaseJsonPayloadInferenceTask
    {
        internal TextToSpeechTask() { }

        public TextToSpeechTask(string input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        public override string Id => "text-to-speech";
    }
}
