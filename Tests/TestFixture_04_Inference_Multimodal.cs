// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference;
using HuggingFace.Inference.Multimodal;
using HuggingFace.Inference.Multimodal.DocumentQuestionAnswering;
using HuggingFace.Inference.Multimodal.ImageToText;
using HuggingFace.Inference.Multimodal.TextToImage;
using HuggingFace.Inference.Multimodal.VisualQuestionAnswering;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace HuggingFace.Tests
{
    internal class TestFixture_04_Inference_Multimodal : AbstractTestFixture
    {
        [Test]
        public async Task Test_01_TextToImage()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var inputs = new TextToImageInputs("An astronaut riding a horse on the moon.")
            {
                NegativePrompt = "low resolution, blurry",
                Height = 1024,
                Width = 1024,
            };
            var task = new TextToImageTask(inputs);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TextToImageTask, TextToImageBinaryResponse>(task);
            Assert.IsNotNull(response);
            Debug.Log(response.CachedPath);
            Assert.IsNotNull(response.Image);
        }

        [Test]
        public async Task Test_02_ImageToText()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7a9ce68183656254495b680e84a86117");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ImageToTextTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ImageToTextTask, ImageToTextResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log(result.GeneratedText);
            }
        }

        [Test]
        public async Task Test_03_VisualQuestionAnswering()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7a9ce68183656254495b680e84a86117");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceQuestionAnsweringInput("What is in this image?", texture);
            var task = new VisualQuestionAnsweringTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<VisualQuestionAnsweringTask, VisualQuestionAnsweringResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Score}: {result.Answer}");
            }
        }

        [Test]
        public async Task Test_04_DocumentQuestionAnswering()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("bc8a52e772933b841a88593462cd36c2");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceQuestionAnsweringInput("What is the idea behind the consumer relations efficiency team?", texture);
            var task = new DocumentQuestionAnsweringTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<DocumentQuestionAnsweringTask, DocumentQuestionAnsweringResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Score}: {result.Answer}");
            }
        }

        [Test]
        public async Task Test_05_01_FeatureExtraction_Text()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var textInput = new FeatureExtractionInput("India, officially the Republic of India, is a country in South Asia.");
            var task = new FeatureExtractionTask(textInput);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<FeatureExtractionTask, FeatureExtractionResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);
        }

        [Test]
        public async Task Test_05_02_FeatureExtraction_Audio()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var audioPath = AssetDatabase.GUIDToAssetPath("6b684332a20988c45933a5a73b22c429");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var audioInput = new FeatureExtractionInput(audioClip);
            audioInput.Parameters = new Dictionary<string, string> { { "truncation", "only_first" } };
            var task = new FeatureExtractionTask(audioInput);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<FeatureExtractionTask, FeatureExtractionResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);
        }

        [Test]
        public async Task Test_05_03_FeatureExtraction_Visual()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7a9ce68183656254495b680e84a86117");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var imageInput = new FeatureExtractionInput(texture);
            imageInput.Parameters = new Dictionary<string, string> { { "truncation", "only_first" } };
            var task = new FeatureExtractionTask(imageInput);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<FeatureExtractionTask, FeatureExtractionResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);
        }
    }
}
