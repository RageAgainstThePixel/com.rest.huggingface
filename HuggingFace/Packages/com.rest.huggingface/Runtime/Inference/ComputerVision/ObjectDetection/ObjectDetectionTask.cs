// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.ComputerVision.ObjectDetection
{
    public sealed class ObjectDetectionTask : BaseImageInferenceTask
    {
        [Preserve]
        public ObjectDetectionTask() { }

        public ObjectDetectionTask(SingleSourceImageInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "object-detection";
    }
}
