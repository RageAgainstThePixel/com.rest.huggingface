using HuggingFace.Hub;
using HuggingFace.Inference.Audio;
using HuggingFace.Inference.Audio.AutomaticSpeechRecognition;
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
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("facebook/fastspeech2-en-ljspeech");
            var task = new TextToSpeechTask("This is a test run", model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<TextToSpeechTask, TextToSpeechResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Debug.Log(result.CachedPath);
        }

        [Test]
        public async Task Test_04_AudioToAudioTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("speechbrain/sepformer-wham");
            var audioPath = AssetDatabase.GUIDToAssetPath("07d1a9fd7238ed941af19229414ce747");
            var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            using var input = new SingleSourceAudioInput(audioClip);
            var task = new AudioToAudioTask(input, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<AudioToAudioTask, AudioToAudioResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.Results);

            foreach (var audioBlob in result.Results)
            {
                Debug.Log(audioBlob.Label);
                Assert.IsFalse(string.IsNullOrWhiteSpace(audioBlob.Blob));
            }
        }
    }
}
