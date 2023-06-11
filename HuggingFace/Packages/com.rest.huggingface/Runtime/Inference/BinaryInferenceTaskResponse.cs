using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HuggingFace.Inference
{
    public abstract class BinaryInferenceTaskResponse : InferenceTaskResponse
    {
        public abstract Task DecodeAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}
