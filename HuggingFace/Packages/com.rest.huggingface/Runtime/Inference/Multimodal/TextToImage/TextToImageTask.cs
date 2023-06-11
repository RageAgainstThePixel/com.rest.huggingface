using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.TextToImage
{
    public sealed class TextToImageTask : BaseJsonPayloadInferenceTask
    {
        public TextToImageTask(TextToImageInputs inputs, ModelInfo model, InferenceOptions options = null)
            : base(model ?? new ModelInfo("stabilityai/stable-diffusion-2-1"), options)
        {
            Inputs = inputs;
        }

        public TextToImageInputs Inputs { get; }

        public override string Id => "text-to-image";

        public override string ToJson(JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(Inputs, settings);
    }
}
