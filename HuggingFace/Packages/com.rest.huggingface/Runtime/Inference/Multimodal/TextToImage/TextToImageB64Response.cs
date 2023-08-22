// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference.Multimodal.TextToImage
{
    public sealed class TextToImageB64Response : B64JsonInferenceTaskResponse
    {
        public TextToImageB64Response(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            blob = JsonConvert.DeserializeObject<B64ImageResult>(content, settings).Blob;
        }

        private readonly string blob;

        public string CachedPath { get; private set; }

        public Texture2D Image { get; private set; }

        public override async Task DecodeAsync(CancellationToken cancellationToken = default)
        {
            await Rest.ValidateCacheDirectoryAsync();
            var localFilePath = Path.Combine(Rest.DownloadCacheDirectory, $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssffff}.jpg");
            CachedPath = localFilePath;

            await using var fileStream = new FileStream(localFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            await fileStream.WriteAsync(Convert.FromBase64String(blob), cancellationToken);

            Image = await Rest.DownloadTextureAsync($"file://{localFilePath}", parameters: null, cancellationToken: cancellationToken);
        }
    }
}
