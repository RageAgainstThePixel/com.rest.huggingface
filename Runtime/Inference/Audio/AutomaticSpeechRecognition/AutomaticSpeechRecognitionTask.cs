// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Audio.AutomaticSpeechRecognition
{
    public sealed class AutomaticSpeechRecognitionTask : BaseAudioInferenceTask
    {
        [Preserve]
        public AutomaticSpeechRecognitionTask() { }

        public AutomaticSpeechRecognitionTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "automatic-speech-recognition";
    }
}
