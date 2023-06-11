using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio.AudioClassification
{
    public sealed class AudioClassificationTask : BaseAudioInferenceTask
    {
        public AudioClassificationTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model ?? new ModelInfo("speechbrain/google_speech_command_xvector"), options)
        {
        }

        public override string Id => "audio-classification";
    }
}