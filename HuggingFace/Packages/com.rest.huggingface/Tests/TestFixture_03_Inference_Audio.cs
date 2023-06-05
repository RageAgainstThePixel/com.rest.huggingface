using HuggingFace.Hub;
using HuggingFace.Inference.Audio;
using NUnit.Framework;
using System.Threading.Tasks;
using HuggingFace.Inference.Audio.AutomaticSpeechRecognition;
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
            var model = new ModelInfo("jonatasgrosman/wav2vec2-large-xlsr-53-english");
            var audioPath = AssetDatabase.GUIDToAssetPath("900a512d644c38c47846d9a6e41961f6");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new SingleSourceAudioInput(audioClip);
            var task = new AutomaticSpeechRecognitionTask(input, model: model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<AutomaticSpeechRecognitionTask, AutomaticSpeechRecognitionResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Result));
            Debug.Log(result.Result);
        }

        [Test]
        public async Task Test_02_AudioClassificationTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("speechbrain/google_speech_command_xvector");
            var audioPath = AssetDatabase.GUIDToAssetPath("6b684332a20988c45933a5a73b22c429");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new SingleSourceAudioInput(audioClip);
            var task = new AudioClassificationTask(input, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<AudioClassificationTask, AudioClassificationResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.Results);

            foreach (var classificationResult in result.Results)
            {
                Debug.Log($"{classificationResult.Label}: {classificationResult.Score}");
            }
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
