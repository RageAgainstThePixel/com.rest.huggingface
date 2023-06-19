// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.ComputerVision.ImageSegmentation
{
    public sealed class ImageSegmentationTask : BaseImageInferenceTask
    {
        [Preserve]
        public ImageSegmentationTask() { }

        public ImageSegmentationTask(SingleSourceImageInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "image-segmentation";
    }
}
