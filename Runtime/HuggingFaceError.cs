// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace
{
    public sealed class HuggingFaceError
    {
        public HuggingFaceError(
            [JsonProperty("error")] string error,
            [JsonProperty("estimated_time")] double estimatedTime
        )
        {
            Error = error;
            EstimatedTime = estimatedTime;
        }

        [JsonProperty("error")]
        public string Error { get; }

        [JsonProperty("estimated_time")]
        public double EstimatedTime { get; }
    }
}
