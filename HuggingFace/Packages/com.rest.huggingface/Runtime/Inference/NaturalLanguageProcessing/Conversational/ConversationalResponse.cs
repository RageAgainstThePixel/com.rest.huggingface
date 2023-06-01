using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Conversational
{
    public sealed class ConversationalResponse : InferenceTaskResponse
    {
        public ConversationalResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Result = JsonConvert.DeserializeObject<Conversation>(content, settings);
        }

        public Conversation Result { get; }
    }
}
