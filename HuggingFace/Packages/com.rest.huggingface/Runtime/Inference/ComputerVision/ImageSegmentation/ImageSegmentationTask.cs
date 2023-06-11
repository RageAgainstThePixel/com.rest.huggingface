using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision.ImageSegmentation
{
    public sealed class ImageSegmentationTask : InferenceTask
    {
        public ImageSegmentationTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "image-segmentation";
    }
}
