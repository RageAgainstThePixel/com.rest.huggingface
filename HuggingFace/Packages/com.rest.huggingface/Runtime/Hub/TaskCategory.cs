// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class TaskCategory
    {
        [JsonConstructor]
        public TaskCategory(
            [JsonProperty("id")] string id,
            [JsonProperty("label")] string label,
            [JsonProperty("subType")] string subType,
            [JsonProperty("type")] string type)
        {
            Id = id;
            Label = label;
            SubType = subType;
            Type = type;
        }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("subType")]
        public string SubType { get; }

        [JsonProperty("type")]
        public string Type { get; }
    }
}
