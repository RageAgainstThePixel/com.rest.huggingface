// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Result
    {
        [JsonConstructor]
        public Result(
            [JsonProperty("task")] ModelTask task,
            [JsonProperty("dataset")] Dataset dataset,
            [JsonProperty("metrics")] List<Metric> metrics)
        {
            Task = task;
            Dataset = dataset;
            Metrics = metrics;
        }

        [JsonProperty("task")]
        public ModelTask Task { get; }

        [JsonProperty("dataset")]
        public Dataset Dataset { get; }

        [JsonProperty("metrics")]
        public IReadOnlyList<Metric> Metrics { get; }
    }
}
