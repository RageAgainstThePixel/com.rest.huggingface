// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using UnityEngine;

namespace HuggingFace.Inference.ComputerVision
{
    public sealed class SingleSourceImageInput : IDisposable
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

        public Stream Image { get; }

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
