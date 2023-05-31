using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HuggingFace.Hub
{
    public class CardData
    {
        [JsonConstructor]
        public CardData(
            [JsonProperty("tag")] string tag,
            [JsonProperty("tags")] List<string> tags,
            [JsonProperty("thumbnail")] string thumbnail,
            [JsonProperty("license")] List<string> license,
            [JsonProperty("language")] string language,
            [JsonProperty("pipeline_tag")] string pipelineTag,
            [JsonProperty("datasets")] List<string> datasets,
            [JsonProperty("widget")] JToken widget,
            [JsonProperty("metrics")] List<string> metrics,
            [JsonProperty("inference")] object inference,
            [JsonProperty("co2_eq_emissions")] long co2EqEmissions,
            [JsonProperty("library_name")] string libraryName,
            [JsonProperty("licence")] List<string> licence,
            [JsonProperty("model-index")] List<ModelIndex> modelIndex,
            [JsonProperty("duplicated_from")] string duplicatedFrom,
            [JsonProperty("library")] List<string> library,
            [JsonProperty("models")] List<string> models)
        {
            Tags = tags;
            Thumbnail = thumbnail;
            License = license;
            Language = language;
            PipelineTag = pipelineTag;
            Datasets = datasets;
            Widget = widget;
            Metrics = metrics;
            Inference = inference;
            Co2EqEmissions = co2EqEmissions;
            LibraryName = libraryName;
            Licence = licence;
            ModelIndex = modelIndex;
            DuplicatedFrom = duplicatedFrom;
            Tag = tag;
            Library = library;
            Models = models;
        }

        [JsonProperty("tags")]
        public IReadOnlyList<string> Tags { get; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; }

        [JsonProperty("license")]
        public IReadOnlyList<string> License { get; }

        [JsonProperty("language")]
        public string Language { get; }

        [JsonProperty("pipeline_tag")]
        public string PipelineTag { get; }

        [JsonProperty("datasets")]
        public IReadOnlyList<string> Datasets { get; }

        [JsonProperty("widget")]
        public JToken Widget { get; }

        [JsonProperty("metrics")]
        public IReadOnlyList<string> Metrics { get; }

        [JsonProperty("inference")]
        public object Inference { get; }

        [JsonProperty("co2_eq_emissions")]
        public long Co2EqEmissions { get; }

        [JsonProperty("library_name")]
        public string LibraryName { get; }

        [JsonProperty("licence")]
        public IReadOnlyList<string> Licence { get; }

        [JsonProperty("model-index")]
        public IReadOnlyList<ModelIndex> ModelIndex { get; }

        [JsonProperty("duplicated_from")]
        public string DuplicatedFrom { get; }

        [JsonProperty("tag")]
        public string Tag { get; }

        [JsonProperty("library")]
        public IReadOnlyList<string> Library { get; }

        [JsonProperty("models")]
        public IReadOnlyList<string> Models { get; }
    }
}
