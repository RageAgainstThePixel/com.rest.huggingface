// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.ComputerVision.ImageClassification
{
    public sealed class ImageClassificationResponse : JsonInferenceTaskResponse
    {
        public ImageClassificationResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ImageClassificationResult>>(content, settings);
        }

        public IReadOnlyList<ImageClassificationResult> Results { get; }
    }
}
