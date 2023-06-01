using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationResponse : InferenceTaskResponse
    {
        public TextGenerationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<TextGenerationResult>>(content, settings);
        }

        public IReadOnlyList<TextGenerationResult> Results { get; }
    }
}
