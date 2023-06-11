using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
