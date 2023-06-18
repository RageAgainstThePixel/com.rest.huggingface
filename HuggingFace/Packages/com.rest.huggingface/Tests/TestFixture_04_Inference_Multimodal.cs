// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference.Multimodal.TextToImage;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HuggingFace.Tests
{
    internal class TestFixture_04_Inference_Multimodal
    {
        [Test]
        public async Task Test_01_TextToImage()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var inputs = new TextToImageInputs("An astronaut riding a horse on the moon.")
            {
                NegativePrompt = "low resolution, blurry",
                Height = 1024,
                Width = 1024,
            };
            var task = new TextToImageTask(inputs);
            var response = await api.InferenceEndpoint.RunInferenceTaskAsync<TextToImageTask, TextToImageResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Image);
        }
    }
}
