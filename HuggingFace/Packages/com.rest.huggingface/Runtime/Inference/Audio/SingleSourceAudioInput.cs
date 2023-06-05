using System;
using System.IO;
using UnityEngine;
using Utilities.Encoding.Wav;

namespace HuggingFace.Inference.Audio
{
    public sealed class SingleSourceAudioInput : IDisposable
    {
        public SingleSourceAudioInput(string audioPath)
            : this(File.OpenRead(audioPath), Path.GetFileName(audioPath))
        {
        }

        public SingleSourceAudioInput(AudioClip audio)
            : this(new MemoryStream(audio.EncodeToWav()), $"{audio.name}.wav")
        {
        }

        public SingleSourceAudioInput(Stream audio, string audioName)
        {
            Audio = audio;

            if (string.IsNullOrWhiteSpace(audioName))
            {
                audioName = "audio.wav";
            }

            AudioName = audioName;
        }

        ~SingleSourceAudioInput()
            => Dispose(false);

        public Stream Audio { get; }

        public string AudioName { get; }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Audio?.Close();
                Audio?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
