// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference;
using HuggingFace.Inference.ComputerVision.ImageClassification;
using HuggingFace.Inference.ComputerVision.ImageSegmentation;
using HuggingFace.Inference.ComputerVision.ImageToImage;
using HuggingFace.Inference.ComputerVision.ObjectDetection;
using HuggingFace.Inference.ComputerVision.ZeroShotImageClassification;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace HuggingFace.Tests
{
    /// <summary>
    /// Test class for Accelerated Inference APIs for Audio processing
    /// A list of tasks and their detailed parameters can be found:
    /// https://huggingface.co/docs/api-inference/detailed_parameters#computer-vision
    /// </summary>
    internal class TestFixture_05_Inference_ComputerVision : AbstractTestFixture
    {
        [Test]
        public async Task Test_01_ImageClassification()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("d9ffac925bd9af349bf0796d2e2e71eb");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ImageClassificationTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ImageClassificationTask, ImageClassificationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label} {result.Score}");
            }
        }

        [Test]
        public async Task Test_02_ObjectDetection()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7b0deca096ae9ea45b2f2b31b38239f3");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ObjectDetectionTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ObjectDetectionTask, ObjectDetectionResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label}: {result.Score}");
            }
        }

        [Test]
        public async Task Test_03_ImageSegmentation()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("7f74c8d6df803ae4399a24dc4bb2fd3b");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var task = new ImageSegmentationTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ImageSegmentationTask, ImageSegmentationResponse>(task);
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
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("8f12b3bc391ad7b4f9dd41a10e5a57e0");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            using var input = new SingleSourceImageInput(texture);
            var param = new ImageToImageParameters("Girl with Pearl Earring");
            var task = new ImageToImageTask(input, param);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ImageToImageTask, ImageToImageResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Image);
        }

        [Test]
        public async Task Test_05_ZeroShotImageClassification()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var imagePath = AssetDatabase.GUIDToAssetPath("4a5097f9cedc0f44a85354f1dd7368dd");
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            var candidateLabels = new List<string>
            {
                "playing music",
                "playing sports"
            };
            var parameters = new ZeroShotClassificationParameters(candidateLabels);
            using var input = new SingleSourceImageInput(texture);
            var task = new ZeroShotImageClassificationTask(input, parameters);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ZeroShotImageClassificationTask, ZeroShotImageClassificationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label} {result.Score}");
            }
        }

        //[Test]
        //public async Task Test_XX_DepthEstimation()
        //{
        //    Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
        //    var imagePath = AssetDatabase.GUIDToAssetPath("0a718d44a1e578148be9a75238a1faaf");
        //    var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
        //    using var input = new SingleSourceImageInput(texture);
        //    var task = new DepthEstimationTask(input, "Intel/dpt-large");
        //    var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<DepthEstimationTask, DepthEstimationResult>(task);
        //    Assert.IsNotNull(response);
        //    Assert.IsNotNull(response.Image);
        //}
    }
}
