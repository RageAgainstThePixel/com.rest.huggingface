using HuggingFace.Hub;
using HuggingFace.Inference.Audio;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace HuggingFace.Tests
{
    /// <summary>
    /// Test class for Accelerated Inference APIs for Audio processing
    /// A list of tasks and their detailed parameters can be found:
    /// https://huggingface.co/docs/api-inference/detailed_parameters#audio
    /// </summary>
    internal class TestFixture_03_Inference_Audio
    {
        [Test]
        public async Task Test_01_AutomaticSpeechRecognitionTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("facebook/wav2vec2-base-960h");
            var audioPath = AssetDatabase.GUIDToAssetPath("900a512d644c38c47846d9a6e41961f6");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new AutomaticSpeechRecognitionInput(audioClip);
            var task = new AutomaticSpeechRecognitionTask(input, model: model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<AutomaticSpeechRecognitionTask, AutomaticSpeechRecognitionResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Result));
            Debug.Log(result.Result);
        }

        [Test]
        public async Task Test_02_AudioClassificationTask()
        {
            await Task.CompletedTask;
        }

        [Test]
        public async Task Test_03_TextToSpeechTask()
        {
            await Task.CompletedTask;
        }

        [Test]
        public async Task Test_04_AudioToAudioTask()
        {
            await Task.CompletedTask;
        }
    }
}
