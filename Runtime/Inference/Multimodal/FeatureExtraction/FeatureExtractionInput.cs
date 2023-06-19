// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HuggingFace.Inference.Multimodal
{
    public sealed class FeatureExtractionInput : IDisposable
    {
        public FeatureExtractionInput(string text)
        {
            Text = text;
        }

        public FeatureExtractionInput(Texture2D texture)
            : this(new SingleSourceImageInput(texture))
        {
        }

        public FeatureExtractionInput(SingleSourceImageInput image)
        {
            Image = image;
        }

        public FeatureExtractionInput(AudioClip audioClip)
            : this(new SingleSourceAudioInput(audioClip))
        {
        }

        public FeatureExtractionInput(SingleSourceAudioInput audio)
        {
            Audio = audio;
        }

        ~FeatureExtractionInput()
            => Dispose(false);

        [JsonProperty("inputs")]
        public string Text { get; internal set; }

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonIgnore]
        public SingleSourceImageInput Image { get; }

        [JsonIgnore]
        public SingleSourceAudioInput Audio { get; }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Audio?.Dispose();
                Image?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
