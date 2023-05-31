using Newtonsoft.Json;

namespace HuggingFace.Inference
{
    public abstract class InferenceTaskResult
    {
        protected InferenceTaskResult(string content, JsonSerializerSettings settings)
        {
            RawContent = content;
            Settings = settings;
        }

        public string RawContent { get; }

        protected JsonSerializerSettings Settings { get; }
    }
}
