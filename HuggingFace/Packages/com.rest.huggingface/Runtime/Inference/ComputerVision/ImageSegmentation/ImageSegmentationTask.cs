using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision
{
    public class ImageSegmentationTask : InferenceTask
    {
        public ImageSegmentationTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "image-segmentation";
    }
}
