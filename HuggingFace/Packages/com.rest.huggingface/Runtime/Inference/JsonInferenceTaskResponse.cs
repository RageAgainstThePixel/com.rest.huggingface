using Newtonsoft.Json;

namespace HuggingFace.Inference
{
    public abstract class JsonInferenceTaskResponse : InferenceTaskResponse
    {
        protected JsonInferenceTaskResponse(string content, JsonSerializerSettings settings)
        {
            RawContent = content;
            Settings = settings;
        }

        public string RawContent { get; }

        protected JsonSerializerSettings Settings { get; }
    }
}
