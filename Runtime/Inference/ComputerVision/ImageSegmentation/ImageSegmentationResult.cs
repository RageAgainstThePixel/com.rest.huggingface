// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using UnityEngine;

namespace HuggingFace.Inference.ComputerVision.ImageSegmentation
{
    public sealed class ImageSegmentationResult : ScoreResults
    {
        [JsonConstructor]
        public ImageSegmentationResult(
            [JsonProperty("score")] double score,
            [JsonProperty("label")] string label,
            [JsonProperty("mask")] string blob)
            : base(label, score)
        {
            Blob = blob;
        }

        [JsonProperty("mask")]
        public string Blob { get; }

        [JsonIgnore]
        public Texture2D Mask { get; internal set; }
    }
}
