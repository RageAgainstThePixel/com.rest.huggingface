using HuggingFace.Hub;

namespace HuggingFace.Inference.Multimodal.ImageToText
{
    public class ImageToTextTask : InferenceTask
    {
        public ImageToTextTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "image-to-text";
    }
}
