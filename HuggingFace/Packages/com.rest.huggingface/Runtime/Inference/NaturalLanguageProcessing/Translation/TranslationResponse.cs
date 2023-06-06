using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Translation
{
    public sealed class TranslationResponse : JsonInferenceTaskResponse
    {
        public TranslationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<TranslationResult>>(content, settings);
        }

        public IReadOnlyList<TranslationResult> Results { get; }
    }
}
