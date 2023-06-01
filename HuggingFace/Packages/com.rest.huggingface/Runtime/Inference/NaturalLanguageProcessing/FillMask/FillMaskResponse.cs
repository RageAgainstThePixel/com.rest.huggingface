using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskResponse : InferenceTaskResponse
    {
        public FillMaskResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Masks = JsonConvert.DeserializeObject<IReadOnlyList<FillMaskResult>>(content, settings);
        }

        public IReadOnlyList<FillMaskResult> Masks { get; }
    }
}
