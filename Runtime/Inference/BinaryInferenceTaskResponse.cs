// Licensed under the MIT License. See LICENSE in the project root for license information.

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
