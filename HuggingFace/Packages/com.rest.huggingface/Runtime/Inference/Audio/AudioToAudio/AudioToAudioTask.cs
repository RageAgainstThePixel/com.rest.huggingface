using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio
{
    public sealed class AudioToAudioTask : InferenceTask
    {
        public AudioToAudioTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "audio-to-audio";
    }
}
