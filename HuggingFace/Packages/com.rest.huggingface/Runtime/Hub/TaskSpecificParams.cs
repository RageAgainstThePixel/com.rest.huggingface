// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class TaskSpecificParams
    {
        [JsonConstructor]
        public TaskSpecificParams([JsonProperty("conversational")] Conversational conversational)
        {
            Conversational = conversational;
        }

        [JsonProperty("conversational")]
        public Conversational Conversational { get; }
    }
}
