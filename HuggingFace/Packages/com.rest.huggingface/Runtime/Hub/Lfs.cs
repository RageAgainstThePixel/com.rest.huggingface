// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Lfs
    {
        [JsonConstructor]
        public Lfs(
            [JsonProperty("sha256")] string sha256,
            [JsonProperty("size")] long size,
            [JsonProperty("pointerSize")] int pointerSize)
        {
            Sha256 = sha256;
            Size = size;
            PointerSize = pointerSize;
        }

        [JsonProperty("sha256")]
        public string Sha256 { get; }

        [JsonProperty("size")]
        public long Size { get; }

        [JsonProperty("pointerSize")]
        public int PointerSize { get; }
    }
}
