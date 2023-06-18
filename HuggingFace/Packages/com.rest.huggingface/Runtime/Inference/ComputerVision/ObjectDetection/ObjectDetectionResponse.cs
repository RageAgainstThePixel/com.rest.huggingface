// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ObjectDetection
{
    public sealed class ObjectDetectionResponse : JsonInferenceTaskResponse
    {
        public ObjectDetectionResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ObjectDetectionResult>>(content, settings);
        }

        public IReadOnlyList<ObjectDetectionResult> Results { get; }
    }
}
