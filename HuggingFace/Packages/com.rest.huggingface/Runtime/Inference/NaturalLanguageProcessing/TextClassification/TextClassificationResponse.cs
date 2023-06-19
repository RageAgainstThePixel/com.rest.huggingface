using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextClassification
{
    public sealed class TextClassificationResponse : JsonInferenceTaskResponse
    {
        public TextClassificationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<IReadOnlyList<ScoreResults>>>(content, settings);
        }

        public IReadOnlyList<IReadOnlyList<ScoreResults>> Results { get; }
    }
}
