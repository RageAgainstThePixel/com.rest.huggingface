// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HuggingFace.Inference.Multimodal
{
    public sealed class SingleSourceQuestionAnsweringInput : SingleSourceImageInput
    {
        public SingleSourceQuestionAnsweringInput(string question, string imagePath) : base(imagePath)
        {
            Question = question;
        }

        public SingleSourceQuestionAnsweringInput(string question, Texture2D image) : base(image)
        {
            Question = question;
        }

        public SingleSourceQuestionAnsweringInput(string question, Stream image, string imageName) : base(image, imageName)
        {
            Question = question;
        }

        [JsonProperty("image")]
        public string B64StringImage { get; private set; }

        [JsonProperty("question")]
        public string Question { get; }

        public async Task<string> ToJson(JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await Image.CopyToAsync(memoryStream, cancellationToken);
            var bytes = memoryStream.ToArray();
            B64StringImage = Convert.ToBase64String(bytes);
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}
