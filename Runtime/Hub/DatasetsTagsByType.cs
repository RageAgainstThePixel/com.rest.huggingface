using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class DatasetsTagsByType
    {
        [JsonConstructor]
        public DatasetsTagsByType(
            [JsonProperty("language")] List<Language> language,
            [JsonProperty("license")] List<License> license,
            [JsonProperty("other")] List<Other> other,
            [JsonProperty("task_ids")] List<TaskId> taskIds,
            [JsonProperty("benchmark")] List<Benchmark> benchmark,
            [JsonProperty("task_categories")] List<TaskCategory> taskCategories,
            [JsonProperty("size_categories")] List<SizeCategory> sizeCategories
        )
        {
            Language = language;
            License = license;
            Other = other;
            TaskIds = taskIds;
            Benchmark = benchmark;
            TaskCategories = taskCategories;
            SizeCategories = sizeCategories;
        }

        [JsonProperty("language")]
        public IReadOnlyList<Language> Language { get; }

        [JsonProperty("license")]
        public IReadOnlyList<License> License { get; }

        [JsonProperty("other")]
        public IReadOnlyList<Other> Other { get; }

        [JsonProperty("task_ids")]
        public IReadOnlyList<TaskId> TaskIds { get; }

        [JsonProperty("benchmark")]
        public IReadOnlyList<Benchmark> Benchmark { get; }

        [JsonProperty("task_categories")]
        public IReadOnlyList<TaskCategory> TaskCategories { get; }

        [JsonProperty("size_categories")]
        public IReadOnlyList<SizeCategory> SizeCategories { get; }
    }
}
