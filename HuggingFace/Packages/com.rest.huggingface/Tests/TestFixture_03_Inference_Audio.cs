// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference;
using HuggingFace.Inference.Audio.AudioClassification;
using HuggingFace.Inference.Audio.AudioToAudio;
using HuggingFace.Inference.Audio.AutomaticSpeechRecognition;
using HuggingFace.Inference.Audio.TextToSpeech;
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
    internal class TestFixture_03_Inference_Audio : AbstractTestFixture
    {
        [Test]
        public async Task Test_01_AutomaticSpeechRecognitionTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var audioPath = AssetDatabase.GUIDToAssetPath("900a512d644c38c47846d9a6e41961f6");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new SingleSourceAudioInput(audioClip);
            var task = new AutomaticSpeechRecognitionTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<AutomaticSpeechRecognitionTask, AutomaticSpeechRecognitionResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Result));
            Debug.Log(response.Result);
        }

        [Test]
        public async Task Test_02_AudioClassificationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var audioPath = AssetDatabase.GUIDToAssetPath("6b684332a20988c45933a5a73b22c429");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new SingleSourceAudioInput(audioClip);
            var task = new AudioClassificationTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<AudioClassificationTask, AudioClassificationResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Debug.Log($"{result.Label}: {result.Score}");
            }
        }

        [Test]
        public async Task Test_03_TextToSpeechTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var task = new TextToSpeechTask("This is a test run", "speechbrain/tts-tacotron2-ljspeech");
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TextToSpeechTask, TextToSpeechResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.AudioClip);
            Debug.Log(response.CachedPath);
        }

        [Test]
        public async Task Test_04_AudioToAudioTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var audioPath = AssetDatabase.GUIDToAssetPath("07d1a9fd7238ed941af19229414ce747");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new SingleSourceAudioInput(audioClip);
            var task = new AudioToAudioTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<AudioToAudioTask, AudioToAudioResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);

            foreach (var audioBlob in response.Results)
            {
                Debug.Log(audioBlob.Label);
                Assert.IsFalse(string.IsNullOrWhiteSpace(audioBlob.Blob));
            }
        }
    }
}
