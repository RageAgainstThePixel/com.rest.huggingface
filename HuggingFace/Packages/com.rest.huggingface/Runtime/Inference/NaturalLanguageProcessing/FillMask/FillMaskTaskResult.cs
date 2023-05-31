using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskTaskResult : InferenceTaskResult
    {
        public FillMaskTaskResult(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Masks = JsonConvert.DeserializeObject<IReadOnlyList<FillMaskResponse>>(content, settings);
        }

        public IReadOnlyList<FillMaskResponse> Masks { get; }
    }
}
