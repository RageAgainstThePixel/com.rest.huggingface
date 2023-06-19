// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Multimodal.TextToImage
{
    public sealed class TextToImageTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public TextToImageTask() { }

        public TextToImageTask(TextToImageInputs inputs, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Inputs = inputs;
        }

        public TextToImageInputs Inputs { get; }

        public override string Id => "text-to-image";

        public override Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
            => Task.FromResult(JsonConvert.SerializeObject(Inputs, settings));
    }
}
