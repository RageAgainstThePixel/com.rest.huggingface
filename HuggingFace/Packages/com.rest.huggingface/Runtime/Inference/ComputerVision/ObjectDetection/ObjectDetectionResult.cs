// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ObjectDetection
{
    public sealed class ObjectDetectionResult
    {
        [JsonConstructor]
        public ObjectDetectionResult(
            [JsonProperty("score")] double score,
            [JsonProperty("label")] string label,
            [JsonProperty("box")] ObjectDetectionBox box
        )
        {
            Score = score;
            Label = label;
            Box = box;
        }

        [JsonProperty("score")]
        public double Score { get; }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("box")]
        public ObjectDetectionBox Box { get; }
    }
}
