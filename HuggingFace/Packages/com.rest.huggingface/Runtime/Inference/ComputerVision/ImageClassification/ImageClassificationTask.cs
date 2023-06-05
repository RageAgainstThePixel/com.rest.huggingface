using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision
{
    public class ImageClassificationTask : InferenceTask
    {
        public ImageClassificationTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "image-classification";
    }
}
