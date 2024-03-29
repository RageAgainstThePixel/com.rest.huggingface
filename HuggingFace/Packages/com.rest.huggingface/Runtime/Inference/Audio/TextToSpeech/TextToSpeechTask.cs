// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Audio.TextToSpeech
{
    public sealed class TextToSpeechTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public TextToSpeechTask() { }

        public TextToSpeechTask(string input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        public override string Id => "text-to-speech";

        public override string MimeType => "audio/mp3";
    }
}
