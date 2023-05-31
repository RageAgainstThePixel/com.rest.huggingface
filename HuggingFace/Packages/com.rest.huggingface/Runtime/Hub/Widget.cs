// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Widget
    {
        [JsonConstructor]
        public Widget(
            [JsonProperty("text")] string text,
            [JsonProperty("context")] string context,
            [JsonProperty("example_title")] string exampleTitle,
            [JsonProperty("structuredData")] object structuredData)
        {
            Text = text;
            Context = context;
            ExampleTitle = exampleTitle;
            StructuredData = structuredData;
        }

        [JsonProperty("text")]
        public string Text { get; }

        [JsonProperty("context")]
        public string Context { get; }

        [JsonProperty("example_title")]
        public string ExampleTitle { get; }

        [JsonProperty("structuredData")]
        public object StructuredData { get; }
    }
}
