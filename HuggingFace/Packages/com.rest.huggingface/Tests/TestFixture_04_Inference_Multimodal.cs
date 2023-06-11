using HuggingFace.Hub;
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
            var model = new ModelInfo("stabilityai/stable-diffusion-2-1");
            var inputs = new TextToImageInputs("An astronaut riding a horse on the moon.")
            {
                NegativePrompt = "low resolution, blurry",
                Height = 1024,
                Width = 1024,
            };
            var task = new TextToImageTask(inputs, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<TextToImageTask, TextToImageResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Image);
        }
    }
}
