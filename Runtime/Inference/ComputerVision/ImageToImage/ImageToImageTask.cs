using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision.ImageToImage
{
    public class ImageToImageTask : InferenceTask
    {
        public ImageToImageTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "image-to-image";
    }
}
