using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HuggingFace.Inference.Audio.AutomaticSpeechRecognition
{
    public sealed class AutomaticSpeechRecognitionResponse : InferenceTaskResponse
    {
        public AutomaticSpeechRecognitionResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            if (JObject.Parse(content).TryGetValue("text", out JToken responseObject))
            {
                Result = responseObject.ToString();
            }
            else
            {
                Result = content;
            }
        }

        public string Result { get; }
    }
}
