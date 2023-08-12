// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities.WebRequestRest;

namespace HuggingFace.Inference.Audio.TextToSpeech
{
    public sealed class TextToSpeechResponse : BinaryInferenceTaskResponse
    {
        public string CachedPath { get; private set; }

        public AudioClip AudioClip { get; private set; }

        public override async Task DecodeAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            await Rest.ValidateCacheDirectoryAsync();
            var filePath = Path.Combine(Rest.DownloadCacheDirectory, $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssffff}.wav");
            Debug.Log(filePath);
            CachedPath = filePath;
            var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);

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
                        break;
                }
            }
            finally
            {
                fileStream.Close();
                await fileStream.DisposeAsync();
            }

            AudioClip = await Rest.DownloadAudioClipAsync($"file://{filePath}", AudioType.WAV, parameters: null, cancellationToken: cancellationToken);
        }
    }
}
