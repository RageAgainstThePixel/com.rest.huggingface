using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HuggingFace.Hub;

namespace HuggingFace.Inference.Audio
{
    public abstract class BaseAudioInferenceTask : InferenceTask
    {
        protected BaseAudioInferenceTask(SingleSourceAudioInput input, ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
            Input = input;
        }

        public SingleSourceAudioInput Input { get; }

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken = default)
        {
            await using var memoryStream = new MemoryStream();
            await Input.Audio.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
