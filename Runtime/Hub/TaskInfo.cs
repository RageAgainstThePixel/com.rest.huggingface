// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class TaskInfo
    {
        [JsonConstructor]
        public TaskInfo(
            [JsonProperty("summary")] string summary,
            [JsonProperty("widgetModels")] List<string> widgetModels,
            [JsonProperty("id")] string id,
            [JsonProperty("label")] string label
        )
        {
            Summary = summary;
            WidgetModels = widgetModels;
            Id = id;
            Label = label;
        }

        [JsonProperty("summary")]
        public string Summary { get; }

        [JsonProperty("widgetModels")]
        public IReadOnlyList<string> WidgetModels { get; }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("label")]
        public string Label { get; }
    }
}
