// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace HuggingFace.Hub
{
    public class ModelInfo
    {
        public ModelInfo(string modelId)
        {
            ModelId = modelId;
        }

        [JsonConstructor]
        public ModelInfo(
            [JsonProperty("modelId")] string modelId,
            [JsonProperty("author")] string author,
            [JsonProperty("sha")] string sha,
            [JsonProperty("lastModified")] DateTime lastModified,
            [JsonProperty("private")] bool @private,
            [JsonProperty("disabled")] bool disabled,
            [JsonProperty("gated")] bool gated,
            [JsonProperty("pipeline_tag")] string pipelineTag,
            [JsonProperty("tags")] List<string> tags,
            [JsonProperty("downloads")] int downloads,
            [JsonProperty("library_name")] string libraryName,
            [JsonProperty("mask_token")] string maskToken,
            [JsonProperty("widgetData")] JToken widgetData,
            [JsonProperty("likes")] int likes,
            [JsonProperty("model-index")] JToken modelIndex,
            [JsonProperty("config")] ModelConfig config,
            [JsonProperty("cardData")] JToken cardData,
            [JsonProperty("transformersInfo")] TransformersInfo transformersInfo,
            [JsonProperty("spaces")] List<string> spaces,
            [JsonProperty("siblings")] List<Sibling> siblings,
            [JsonProperty("securityStatus")] SecurityStatus securityStatus)
        {
            ModelId = modelId;
            Author = author;
            Sha = sha;
            LastModified = lastModified;
            Private = @private;
            Disabled = disabled;
            Gated = gated;
            PipelineTag = pipelineTag;
            Tags = tags;
            Downloads = downloads;
            LibraryName = libraryName;
            MaskToken = maskToken;
            WidgetData = widgetData;
            Likes = likes;
            ModelIndex = modelIndex;
            Config = config;
            CardData = cardData;
            TransformersInfo = transformersInfo;
            Spaces = spaces;
            Siblings = siblings;
            SecurityStatus = securityStatus;
        }

        public static implicit operator ModelInfo(string modelId) => new ModelInfo(modelId);

        [JsonProperty("modelId")]
        public string ModelId { get; }

        [JsonProperty("author")]
        public string Author { get; }

        [JsonProperty("sha")]
        public string Sha { get; }

        [JsonProperty("lastModified")]
        public DateTime LastModified { get; }

        [JsonProperty("private")]
        public bool Private { get; }

        [JsonProperty("disabled")]
        public bool Disabled { get; }

        [JsonProperty("gated")]
        public bool Gated { get; }

        [JsonProperty("pipeline_tag")]
        public string PipelineTag { get; }

        [JsonProperty("tags")]
        public IReadOnlyList<string> Tags { get; }

        [JsonProperty("downloads")]
        public int Downloads { get; }

        [JsonProperty("library_name")]
        public string LibraryName { get; }

        [JsonProperty("mask_token")]
        public string MaskToken { get; }

        [JsonProperty("widgetData")]
        public JToken WidgetData { get; }

        [JsonProperty("likes")]
        public int Likes { get; }

        [JsonProperty("model-index")]
        public JToken ModelIndex { get; }

        [JsonProperty("config")]
        public ModelConfig Config { get; }

        [JsonProperty("cardData")]
        public JToken CardData { get; }

        [JsonProperty("transformersInfo")]
        public TransformersInfo TransformersInfo { get; }

        [JsonProperty("spaces")]
        public IReadOnlyList<string> Spaces { get; }

        [JsonProperty("siblings")]
        public IReadOnlyList<Sibling> Siblings { get; }

        [JsonProperty("securityStatus")]
        public SecurityStatus SecurityStatus { get; }
    }
}
