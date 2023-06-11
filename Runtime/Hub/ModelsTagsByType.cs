// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public sealed class ModelsTagsByType
    {
        [JsonConstructor]
        public ModelsTagsByType(
            [JsonProperty("library")] IReadOnlyList<Library> library,
            [JsonProperty("other")] IReadOnlyList<Other> other,
            [JsonProperty("license")] IReadOnlyList<License> license,
            [JsonProperty("language")] IReadOnlyList<Language> language,
            [JsonProperty("dataset")] IReadOnlyList<DatasetInfo> dataset,
            [JsonProperty("pipeline_tag")] IReadOnlyList<PipelineTag> pipelineTag)
        {
            Library = library;
            Other = other;
            License = license;
            Language = language;
            Dataset = dataset;
            PipelineTag = pipelineTag;
        }

        [JsonProperty("library")]
        public IReadOnlyList<Library> Library { get; }

        [JsonProperty("other")]
        public IReadOnlyList<Other> Other { get; }

        [JsonProperty("license")]
        public IReadOnlyList<License> License { get; }

        [JsonProperty("language")]
        public IReadOnlyList<Language> Language { get; }

        [JsonProperty("dataset")]
        public IReadOnlyList<DatasetInfo> Dataset { get; }

        [JsonProperty("pipeline_tag")]
        public IReadOnlyList<PipelineTag> PipelineTag { get; }
    }
}
