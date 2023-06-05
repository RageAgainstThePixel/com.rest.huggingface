using System;
using System.IO;
using UnityEngine;
using Utilities.Encoding.Wav;

namespace HuggingFace.Inference.Audio
{
    public sealed class AutomaticSpeechRecognitionInput : IDisposable
    {
        public AutomaticSpeechRecognitionInput(string audioPath)
            : this(File.OpenRead(audioPath), Path.GetFileName(audioPath))
        {
        }

        public AutomaticSpeechRecognitionInput(AudioClip audio)
            : this(new MemoryStream(audio.EncodeToWav()), $"{audio.name}.wav")
        {
        }

        public AutomaticSpeechRecognitionInput(Stream audio, string audioName)
        {
            Audio = audio;

            if (string.IsNullOrWhiteSpace(audioName))
            {
                audioName = "audio.wav";
            }

            AudioName = audioName;
        }

        ~AutomaticSpeechRecognitionInput()
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