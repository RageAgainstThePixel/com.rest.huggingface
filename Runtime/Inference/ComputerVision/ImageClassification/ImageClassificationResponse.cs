// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.ComputerVision.ImageClassification
{
    public sealed class ImageClassificationResponse : JsonInferenceTaskResponse
    {
        public ImageClassificationResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ScoreResults>>(content, settings);
        }

        public IReadOnlyList<ScoreResults> Results { get; }
    }
}
