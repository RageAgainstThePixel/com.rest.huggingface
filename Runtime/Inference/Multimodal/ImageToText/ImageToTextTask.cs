// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Multimodal.ImageToText
{
    public sealed class ImageToTextTask : BaseImageInferenceTask
    {
        [Preserve]
        public ImageToTextTask() { }

        public ImageToTextTask(SingleSourceImageInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "image-to-text";
    }
}
