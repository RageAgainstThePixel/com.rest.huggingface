// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference.ComputerVision;
using HuggingFace.Inference.ComputerVision.ImageClassification;
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
            var imagePath = AssetDatabase.GUIDToAssetPath("32291e451b73587408924bd1f44e60ed");
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
    }
}
