// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference.ComputerVision.ImageSegmentation
{
    public sealed class ImageSegmentationResponse : B64JsonInferenceTaskResponse
    {
        public ImageSegmentationResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ImageSegmentationResult>>(content, settings);
        }

        public IReadOnlyList<ImageSegmentationResult> Results { get; }

        public override async Task DecodeAsync(CancellationToken cancellationToken = default)
            => await Task.WhenAll(Results.Select(result => DecodeImageAsync(result, cancellationToken)).ToList());

        private static async Task DecodeImageAsync(ImageSegmentationResult result, CancellationToken cancellationToken)
        {
            await Rest.ValidateCacheDirectoryAsync();
            Rest.TryGetDownloadCacheItem(result.Blob, out var guid);
            var localFilePath = Path.Combine(Rest.DownloadCacheDirectory, $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssffff}-{guid}.jpg");
            await using var fileStream = new FileStream(localFilePath, FileMode.CreateNew, FileAccess.Read, FileShare.None);
            await fileStream.WriteAsync(Convert.FromBase64String(result.Blob), cancellationToken);
            result.Mask = await Rest.DownloadTextureAsync($"file://{localFilePath}", parameters: null, cancellationToken: cancellationToken);
        }
    }
}
