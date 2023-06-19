// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference;
using HuggingFace.Inference.Multimodal.ImageToText;
using HuggingFace.Inference.Multimodal.TextToImage;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

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

        [Test]
        public async Task Test_02_ImageToText()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7a9ce68183656254495b680e84a86117");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ImageToTextTask(input);
            var response = await api.InferenceEndpoint.RunInferenceTaskAsync<ImageToTextTask, ImageToTextResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log(result.GeneratedText);
            }
        }
    }
}
