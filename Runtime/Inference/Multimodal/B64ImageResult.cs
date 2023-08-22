// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Inference.Multimodal
{
    public sealed class B64ImageResult
    {
        [JsonConstructor]
        public B64ImageResult([JsonProperty("image")] string blob)
        {
            Blob = blob;
        }

        [JsonProperty("image")]
        public string Blob { get; }
    }
}
