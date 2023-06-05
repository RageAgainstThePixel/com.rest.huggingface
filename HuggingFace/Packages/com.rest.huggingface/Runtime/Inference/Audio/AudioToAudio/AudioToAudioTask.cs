using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio
{
    public class AudioToAudioTask : InferenceTask
    {
        public AudioToAudioTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "audio-to-audio";
    }
}
