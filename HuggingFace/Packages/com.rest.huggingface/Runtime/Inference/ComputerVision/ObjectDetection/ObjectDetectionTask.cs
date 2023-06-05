using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision
{
    public class ObjectDetectionTask : InferenceTask
    {
        public ObjectDetectionTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "object-detection";
    }
}
