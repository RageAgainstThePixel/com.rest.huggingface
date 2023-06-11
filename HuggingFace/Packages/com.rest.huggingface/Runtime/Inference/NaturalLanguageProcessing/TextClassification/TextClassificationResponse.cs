using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextClassification
{
    public sealed class TextClassificationResponse : JsonInferenceTaskResponse
    {
        public TextClassificationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<IReadOnlyList<TextClassificationResult>>>(content, settings);
        }

        public IReadOnlyList<IReadOnlyList<TextClassificationResult>> Results { get; }
    }
}
