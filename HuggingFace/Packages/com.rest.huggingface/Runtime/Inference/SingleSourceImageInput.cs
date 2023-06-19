// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace HuggingFace.Inference
{
    public class SingleSourceImageInput : IDisposable
    {
        public SingleSourceImageInput(string imagePath)
            : this(File.OpenRead(imagePath), Path.GetFileName(imagePath))
        {
        }

        public SingleSourceImageInput(Texture2D image)
            : this(new MemoryStream(image.EncodeToPNG()), $"{image.name}.png")
        {
        }

        public SingleSourceImageInput(Stream image, string imageName)
        {
            Image = image;

            if (string.IsNullOrWhiteSpace(imageName))
            {
                imageName = "image.png";
            }

            ImageName = imageName;
        }

        ~SingleSourceImageInput()
            => Dispose(false);

        [JsonIgnore]
        public Stream Image { get; }

        [JsonIgnore]
        public string ImageName { get; }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Image?.Close();
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
