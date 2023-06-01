using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.ZeroShotClassification
{
    public sealed class ZeroShotClassificationResponse : InferenceTaskResponse
    {
        public ZeroShotClassificationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ZeroShotClassificationResult>>(content, settings);
        }

        public IReadOnlyList<ZeroShotClassificationResult> Results { get; }
    }
}
