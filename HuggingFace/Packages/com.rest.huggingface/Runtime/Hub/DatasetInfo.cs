using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class DatasetInfo
    {
        [JsonConstructor]
        public DatasetInfo(
            [JsonProperty("id")] string id,
            [JsonProperty("label")] string label,
            [JsonProperty("type")] string type)
        {
            Id = id;
            Label = label;
            Type = type;
        }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("type")]
        public string Type { get; }
    }
}