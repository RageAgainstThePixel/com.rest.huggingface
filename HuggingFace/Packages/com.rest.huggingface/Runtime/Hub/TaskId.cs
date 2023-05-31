// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class TaskId
    {
        [JsonConstructor]
        public TaskId(
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
