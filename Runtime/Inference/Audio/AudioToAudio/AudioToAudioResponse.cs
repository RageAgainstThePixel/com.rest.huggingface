// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference.Audio.AudioToAudio
{
    public sealed class AudioToAudioResponse : B64JsonInferenceTaskResponse
    {
        /// <inheritdoc />
        public AudioToAudioResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<AudioToAudioResult>>(content, settings);
        }

        public IReadOnlyList<AudioToAudioResult> Results { get; }

        /// <inheritdoc />
        public override async Task DecodeAsync(CancellationToken cancellationToken = default)
            => await Task.WhenAll(Results.Select(result => DecodeAudioAsync(result, cancellationToken)).ToList());

        private static async Task DecodeAudioAsync(AudioToAudioResult result, CancellationToken cancellationToken)
        {
            await Rest.ValidateCacheDirectoryAsync();
            Rest.TryGetDownloadCacheItem(result.Blob, out var guid);
            var localFilePath = Path.Combine(Rest.DownloadCacheDirectory, $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssffff}-{Path.GetFileName(guid)}.mp3");
            var fileStream = new FileStream(localFilePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);

            try
            {
                await fileStream.WriteAsync(Convert.FromBase64String(result.Blob), cancellationToken);
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

            result.AudioClip = await Rest.DownloadAudioClipAsync($"file://{localFilePath}", AudioType.MPEG, parameters: null, cancellationToken: cancellationToken);
        }
    }
}
