// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal
{
    public sealed class FeatureExtractionResponse : JsonInferenceTaskResponse
    {
        public FeatureExtractionResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<IReadOnlyList<IReadOnlyList<double>>>>(content, settings);
        }

        public IReadOnlyList<IReadOnlyList<IReadOnlyList<double>>> Results { get; }
    }
}
