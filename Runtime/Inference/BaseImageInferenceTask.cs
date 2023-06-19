// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HuggingFace.Inference
{
    public abstract class BaseImageInferenceTask : InferenceTask
    {
        protected BaseImageInferenceTask() { }

        protected BaseImageInferenceTask(SingleSourceImageInput input, ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
            Input = input;
        }

        public SingleSourceImageInput Input { get; }

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await Input.Image.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
