// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class ModelIndex
    {
        [JsonConstructor]
        public ModelIndex(
            [JsonProperty("name")] string name,
            [JsonProperty("results")] List<Result> results)
        {
            Name = name;
            Results = results;
        }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("results")]
        public IReadOnlyList<Result> Results { get; }
    }
}
