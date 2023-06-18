// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

            if (!Rest.TryGetDownloadCacheItem(result.Blob, out var localFilePath))
            {
                await using var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite);
                await fileStream.WriteAsync(Convert.FromBase64String(result.Blob), cancellationToken);
            }

            result.AudioClip = await Rest.DownloadAudioClipAsync(localFilePath, AudioType.WAV, parameters: null, cancellationToken: cancellationToken);
        }
    }
}
