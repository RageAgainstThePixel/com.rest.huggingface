// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio.AutomaticSpeechRecognition
{
    public sealed class AutomaticSpeechRecognitionTask : BaseAudioInferenceTask
    {
        internal AutomaticSpeechRecognitionTask() { }

        public AutomaticSpeechRecognitionTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model ?? new ModelInfo("jonatasgrosman/wav2vec2-large-xlsr-53-english"), options)
        {
        }

        public override string Id => "automatic-speech-recognition";
    }
}
