using HuggingFace;
using HuggingFace.Hub;
using HuggingFace.Inference.NaturalLanguageProcessing;
using HuggingFace.Inference.NaturalLanguageProcessing.FillMask;
using HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;

namespace Rest.HuggingFace.Tests
{
    /// <summary>
    /// Test class for Accelerated Inference APIs
    /// A list of tasks and their detailed parameters can be found:
    /// https://huggingface.co/docs/api-inference/detailed_parameters
    /// </summary>
    internal class TestFixture_02_Inference
    {
        [Test]
        public async Task Test_01_FillMaskTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("bert-base-uncased");
            var task = new FillMaskInferenceTask("The answer to the universe is [MASK].", model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<FillMaskInferenceTask, FillMaskTaskResult>(task);
            Assert.IsNotNull(result);

            foreach (var resultMask in result.Masks)
            {
                Debug.Log(resultMask.Sequence);
            }
        }

        [Test]
        public async Task Test_02_SummarizationTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("facebook/bart-large-cnn");
            var task = new SummarizationInferenceTask("The tower is 324 metres (1,063 ft) tall, about the same height as an 81-storey building, and the tallest structure in Paris. Its base is square, measuring 125 metres (410 ft) on each side. During its construction, the Eiffel Tower surpassed the Washington Monument to become the tallest man-made structure in the world, a title it held for 41 years until the Chrysler Building in New York City was finished in 1930. It was the first structure to reach a height of 300 metres. Due to the addition of a broadcasting aerial at the top of the tower in 1957, it is now taller than the Chrysler Building by 5.2 metres (17 ft). Excluding transmitters, the Eiffel Tower is the second tallest free-standing structure in France after the Millau Viaduct.", model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<SummarizationInferenceTask, SummarizationTaskResult>(task);
            Assert.IsNotNull(result);

            foreach (var summary in result.Summaries)
            {
                Debug.Log(summary.Text);
            }
        }

        [Test]
        public async Task Test_03_QuestionAnsweringTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("deepset/xlm-roberta-large-squad2");
            var input = new QuestionAnsweringInput("What's my name?", "My name is Clara and I live in Berkeley.");
            var task = new QuestionAnsweringTask(input, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<QuestionAnsweringTask, QuestionAnsweringTaskResult>(task);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Debug.Log(result.Result.Answer);
        }
    }
}