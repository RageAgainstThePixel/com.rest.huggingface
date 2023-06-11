// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class Sibling
    {
        [JsonConstructor]
        public Sibling(
            [JsonProperty("rfilename")] string rfilename,
            [JsonProperty("blobId")] string blobId,
            [JsonProperty("size")] long size,
            [JsonProperty("size (at ?)")] long? sizeAt,
            [JsonProperty("lfs")] Lfs lfs
        )
        {
            Rfilename = rfilename;
            BlobId = blobId;
            Size = size;
            SizeAt = sizeAt;
            Lfs = lfs;
        }

        [JsonProperty("rfilename")]
        public string Rfilename { get; }

        [JsonProperty("blobId")]
        public string BlobId { get; }

        [JsonProperty("size")]
        public long Size { get; }

        [JsonProperty("size (at ?)")]
        public long? SizeAt { get; }

        [JsonProperty("lfs")]
        public Lfs Lfs { get; }
    }
}
