using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision.DepthEstimation
{
    public class DepthEstimationTask : InferenceTask
    {
        public DepthEstimationTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "depth-estimation";
    }
}
