// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HuggingFace.Inference
{
    public abstract class B64JsonInferenceTaskResponse : JsonInferenceTaskResponse
    {
        protected B64JsonInferenceTaskResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
        }

        public abstract Task DecodeAsync(CancellationToken cancellationToken = default);
    }
}
