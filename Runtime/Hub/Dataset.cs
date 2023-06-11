using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Dataset
    {
        [JsonConstructor]
        public Dataset(
            [JsonProperty("type")] string type,
            [JsonProperty("name")] string name)
        {
            Type = type;
            Name = name;
        }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("name")]
        public string Name { get; }
    }
}