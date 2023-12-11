// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Audio.AudioToAudio
{
    public sealed class AudioToAudioTask : BaseAudioInferenceTask
    {
        [Preserve]
        public AudioToAudioTask() { }

        public AudioToAudioTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "audio-to-audio";

        public override string MimeType => "audio/mp3";
    }
}
