using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference
{
    public abstract class BaseJsonPayloadInferenceTask : InferenceTask
    {
        protected BaseJsonPayloadInferenceTask() { }

        protected BaseJsonPayloadInferenceTask(ModelInfo model, InferenceOptions options)
            : base(model, options)
        {
        }

        public override string ToJson(JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(this, settings);
    }
}
