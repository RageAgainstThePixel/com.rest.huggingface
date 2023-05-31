// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class WidgetDatum
    {
        [JsonConstructor]
        public WidgetDatum([JsonProperty("text")] string text)
        {
            Text = text;
        }

        [JsonProperty("text")]
        public string Text { get; }
    }
}
