using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio
{
    public sealed class AudioClassificationTask : InferenceTask
    {
        public AudioClassificationTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "audio-classification";
    }
}
