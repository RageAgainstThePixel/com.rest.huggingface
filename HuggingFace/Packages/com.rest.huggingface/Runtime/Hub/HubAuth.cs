// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class HubAuth
    {
        [JsonConstructor]
        public HubAuth([JsonProperty("type")] string type)
        {
            Type = type;
        }

        [JsonProperty("type")]
        public string Type { get; }
    }
}
