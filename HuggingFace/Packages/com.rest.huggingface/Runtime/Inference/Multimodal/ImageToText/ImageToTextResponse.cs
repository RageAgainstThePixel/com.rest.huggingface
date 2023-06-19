// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.ImageToText
{
    public sealed class ImageToTextResponse : JsonInferenceTaskResponse
    {
        public ImageToTextResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ImageToTextResult>>(content, settings);
        }

        public IReadOnlyList<ImageToTextResult> Results { get; }
    }
}
