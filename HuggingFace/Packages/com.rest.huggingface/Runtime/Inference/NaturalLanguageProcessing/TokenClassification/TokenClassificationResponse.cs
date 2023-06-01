using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification
{
    public sealed class TokenClassificationResponse : InferenceTaskResponse
    {
        public TokenClassificationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<IReadOnlyList<TokenClassificationResult>>>(content, settings);
        }

        public IReadOnlyList<IReadOnlyList<TokenClassificationResult>> Results { get; }
    }
}
