// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.ComputerVision.ImageToImage
{
    public sealed class ImageToImageTask : BaseImageInferenceTask
    {
        [Preserve]
        public ImageToImageTask() { }

        public ImageToImageTask(SingleSourceImageInput input, ImageToImageParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
            Parameters = parameters;
        }

        public ImageToImageParameters Parameters { get; }

        public override string Id => "image-to-image";

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await Input.Image.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }

        public override async Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            if (Parameters == null) { return string.Empty; }

            var bytes = await ToByteArrayAsync(cancellationToken);
            Parameters.Input = Convert.ToBase64String(bytes);
            return JsonConvert.SerializeObject(Parameters, settings);
        }
    }
}
