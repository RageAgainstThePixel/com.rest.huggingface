using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio.AudioToAudio
{
    public sealed class AudioToAudioTask : BaseAudioInferenceTask
    {
        public AudioToAudioTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "audio-to-audio";
    }
}
