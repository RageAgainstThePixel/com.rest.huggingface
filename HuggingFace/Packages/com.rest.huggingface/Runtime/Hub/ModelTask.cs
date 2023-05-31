// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class ModelTask
    {
        [JsonConstructor]
        public ModelTask(
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
