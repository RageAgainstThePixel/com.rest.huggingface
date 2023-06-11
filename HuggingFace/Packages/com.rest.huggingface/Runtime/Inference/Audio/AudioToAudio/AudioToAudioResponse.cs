using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference.Audio
{
    public sealed class AudioToAudioResponse : B64JsonInferenceTaskResponse
    {
        /// <inheritdoc />
        public AudioToAudioResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<AudioInfo>>(content, settings);
        }

        public IReadOnlyList<AudioInfo> Results { get; }

        /// <inheritdoc />
        public override async Task DecodeAsync(CancellationToken cancellationToken = default)
            => await Task.WhenAll(Results.Select(audioInfo => DecodeAudioAsync(audioInfo, cancellationToken)).ToList());

        private static async Task DecodeAudioAsync(AudioInfo audioInfo, CancellationToken cancellationToken)
        {
            await Rest.ValidateCacheDirectoryAsync();

            if (!Rest.TryGetDownloadCacheItem(audioInfo.Blob, out var localFilePath))
            {
                await using var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite);
                await fileStream.WriteAsync(Convert.FromBase64String(audioInfo.Blob), cancellationToken);
            }

            audioInfo.AudioClip = await Rest.DownloadAudioClipAsync($"file://{localFilePath}", AudioType.WAV, parameters: null, cancellationToken: cancellationToken);
        }
    }
}