// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HuggingFace.Hub;

namespace HuggingFace.Inference
{
    public abstract class BaseAudioInferenceTask : InferenceTask
    {
        protected BaseAudioInferenceTask() { }

        protected BaseAudioInferenceTask(SingleSourceAudioInput input, ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
            Input = input;
        }

        public SingleSourceAudioInput Input { get; }

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await Input.Audio.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
