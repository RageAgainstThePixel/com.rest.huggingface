// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.ComputerVision.ImageClassification
{
    public sealed class ImageClassificationTask : BaseImageInferenceTask
    {
        [Preserve]
        public ImageClassificationTask() { }

        public ImageClassificationTask(SingleSourceImageInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "image-classification";
    }
}
