using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Audio
{
    public sealed class AudioClassificationTask : BaseAudioInferenceTask
    {
        public AudioClassificationTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model ?? new ModelInfo("speechbrain/google_speech_command_xvector"), options)
        {
        }

        public override string Id => "audio-classification";
    }

    public sealed class AudioClassificationResponse : InferenceTaskResponse
    {
        public AudioClassificationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
        }
    }
}
