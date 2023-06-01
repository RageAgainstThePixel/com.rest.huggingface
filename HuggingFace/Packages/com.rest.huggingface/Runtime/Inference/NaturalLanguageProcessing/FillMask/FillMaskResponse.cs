using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskResponse : InferenceTaskResponse
    {
        public FillMaskResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            try
            {
                var masks = JsonConvert.DeserializeObject<IReadOnlyList<FillMaskResult>>(content, settings);
                Results = new List<IReadOnlyList<FillMaskResult>> { masks };
            }
            catch (Exception)
            {
                try
                {
                    Results = JsonConvert.DeserializeObject<IReadOnlyList<IReadOnlyList<FillMaskResult>>>(content, settings);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public IReadOnlyList<IReadOnlyList<FillMaskResult>> Results { get; }
    }
}
