using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Conversational
    {
        [JsonConstructor]
        public Conversational(
            [JsonProperty("max_length")] int maxLength
        )
        {
            MaxLength = maxLength;
        }

        [JsonProperty("max_length")]
        public int MaxLength { get; }
    }
}