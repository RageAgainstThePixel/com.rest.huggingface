using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Metric
    {
        [JsonConstructor]
        public Metric(
            [JsonProperty("type")] string type,
            [JsonProperty("value")] double value,
            [JsonProperty("name")] string name,
            [JsonProperty("verified")] bool verified)
        {
            Type = type;
            Value = value;
            Name = name;
            Verified = verified;
        }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("value")]
        public double Value { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("verified")]
        public bool Verified { get; }
    }
}