// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference;
using HuggingFace.Inference.ComputerVision.ImageClassification;
using HuggingFace.Inference.ComputerVision.ImageSegmentation;
using HuggingFace.Inference.ComputerVision.ImageToImage;
using HuggingFace.Inference.ComputerVision.ObjectDetection;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace HuggingFace.Tests
{
    internal class TestFixture_05_Inference_ComputerVision
    {
        [Test]
        public async Task Test_01_ImageClassification()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("d9ffac925bd9af349bf0796d2e2e71eb");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ImageClassificationTask(input);
            var response = await api.InferenceEndpoint.RunInferenceTaskAsync<ImageClassificationTask, ImageClassificationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label} {result.Score}");
            }
        }

        [Test]
        public async Task Test_02_ObjectDetection()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7b0deca096ae9ea45b2f2b31b38239f3");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ObjectDetectionTask(input);
            var response = await api.InferenceEndpoint.RunInferenceTaskAsync<ObjectDetectionTask, ObjectDetectionResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label}: {result.Score}");
            }
        }

        [Test]
        public async Task Test_03_ImageSegmentation()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7f74c8d6df803ae4399a24dc4bb2fd3b");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ImageSegmentationTask(input);
            var response = await api.InferenceEndpoint.RunInferenceTaskAsync<ImageSegmentationTask, ImageSegmentationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label}: {result.Score}");
                Assert.IsNotNull(result.Mask);
            }
        }

        [Test]
        public async Task Test_04_ImageToImage()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("8f12b3bc391ad7b4f9dd41a10e5a57e0");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var param = new ImageToImageParameters("Girl with Pearl Earring");
            var task = new ImageToImageTask(input, param);
            var response = await api.InferenceEndpoint.RunInferenceTaskAsync<ImageToImageTask, ImageToImageResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Image);
        }
    }
}
