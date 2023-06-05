using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio
{
    public class TextToSpeechTask : BaseJsonPayloadInferenceTask
    {
        public TextToSpeechTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "text-to-speech";
    }
}
