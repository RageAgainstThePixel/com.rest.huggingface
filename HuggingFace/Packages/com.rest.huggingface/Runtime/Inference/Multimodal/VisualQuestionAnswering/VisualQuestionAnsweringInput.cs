// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace HuggingFace.Inference.Multimodal.VisualQuestionAnswering
{
    public sealed class VisualQuestionAnsweringInput : SingleSourceImageInput
    {
        public VisualQuestionAnsweringInput(string question, string imagePath) : base(imagePath)
        {
            Question = question;
        }

        public VisualQuestionAnsweringInput(string question, Texture2D image) : base(image)
        {
            Question = question;
        }

        public VisualQuestionAnsweringInput(string question, Stream image, string imageName) : base(image, imageName)
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
