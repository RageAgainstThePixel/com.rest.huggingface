// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ObjectDetection
{
    public sealed class ObjectDetectionBox
    {
        [JsonConstructor]
        public ObjectDetectionBox(
            [JsonProperty("xmin")] int xmin,
            [JsonProperty("ymin")] int ymin,
            [JsonProperty("xmax")] int xmax,
            [JsonProperty("ymax")] int ymax
        )
        {
            Xmin = xmin;
            Ymin = ymin;
            Xmax = xmax;
            Ymax = ymax;
        }

        [JsonProperty("xmin")]
        public int Xmin { get; }

        [JsonProperty("ymin")]
        public int Ymin { get; }

        [JsonProperty("xmax")]
        public int Xmax { get; }

        [JsonProperty("ymax")]
        public int Ymax { get; }
    }
}
