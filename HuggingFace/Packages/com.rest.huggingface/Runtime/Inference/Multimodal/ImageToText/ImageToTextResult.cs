// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal.ImageToText
{
    public sealed class ImageToTextResult
    {
        [JsonConstructor]
        public ImageToTextResult(
            [JsonProperty("generated_text")] string generatedText
        )
        {
            GeneratedText = generatedText;
        }

        [JsonProperty("generated_text")]
        public string GeneratedText { get; }
    }
}
