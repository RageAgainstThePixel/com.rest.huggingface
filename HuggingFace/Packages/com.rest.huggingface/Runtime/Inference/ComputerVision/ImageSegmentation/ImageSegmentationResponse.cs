// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

            if (!Rest.TryGetDownloadCacheItem(result.Blob, out var localFilePath))
            {
                await using var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite);
                await fileStream.WriteAsync(Convert.FromBase64String(result.Blob), cancellationToken);
            }

            result.Mask = await Rest.DownloadTextureAsync(localFilePath, parameters: null, cancellationToken: cancellationToken);
        }
    }
}
