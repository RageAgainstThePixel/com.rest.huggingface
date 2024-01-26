// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference.ComputerVision.ImageToImage
{
    public sealed class ImageToImageResponse : BinaryInferenceTaskResponse
    {
        public string CachedPath { get; private set; }

        public Texture2D Image { get; private set; }

        public override async Task DecodeAsync(Stream stream, bool debug = false, CancellationToken cancellationToken = default)
        {
            await Rest.ValidateCacheDirectoryAsync();
            var filePath = Path.Combine(Rest.DownloadCacheDirectory, $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssffff}.jpg");
            Debug.Log(filePath);
            CachedPath = filePath;
            var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);

            try
            {
                await stream.CopyToAsync(fileStream, cancellationToken);
                await fileStream.FlushAsync(cancellationToken);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case TaskCanceledException:
                    case OperationCanceledException:
                        throw;
                    default:
                        Debug.LogError(e);
                        throw;
                }
            }
            finally
            {
                fileStream.Close();
                await fileStream.DisposeAsync();
            }

            Image = await Rest.DownloadTextureAsync($"file://{filePath}", parameters: new RestParameters(debug: debug), cancellationToken: cancellationToken);
        }
    }
}
