using HuggingFace.Hub;

namespace HuggingFace.Inference.Multimodal.VisualQuestionAnswering
{
    public class VisualQuestionAnsweringTask : InferenceTask
    {
        public VisualQuestionAnsweringTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string Id => "visual-question-answering";
    }
}
