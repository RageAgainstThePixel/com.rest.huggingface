// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;

namespace HuggingFace.Inference.ComputerVision.ImageSegmentation
{
    public sealed class ImageSegmentationTask : BaseImageInferenceTask
    {
        public ImageSegmentationTask(SingleSourceImageInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "image-segmentation";
    }
}
