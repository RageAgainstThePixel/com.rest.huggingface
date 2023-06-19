// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ObjectDetection
{
    public sealed class ObjectDetectionResult : ScoreResults
    {
        [JsonConstructor]
        public ObjectDetectionResult(
            [JsonProperty("score")] double score,
            [JsonProperty("label")] string label,
            [JsonProperty("box")] ObjectDetectionBox box)
            : base(label, score)
        {
            Box = box;
        }

        [JsonProperty("box")]
        public ObjectDetectionBox Box { get; }
    }
}
