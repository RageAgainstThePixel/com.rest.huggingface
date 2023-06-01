using HuggingFace;
using HuggingFace.Hub;
using HuggingFace.Inference.NaturalLanguageProcessing;
using HuggingFace.Inference.NaturalLanguageProcessing.FillMask;
using HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering;
using HuggingFace.Inference.NaturalLanguageProcessing.SentenceSimilarity;
using HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering;
using HuggingFace.Inference.NaturalLanguageProcessing.TextClassification;
using HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration;
using HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification;
using NUnit.Framework;
using System.Collections.Generic;
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
            var task = new FillMaskTask("The answer to the universe is [MASK].", model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<FillMaskTask, FillMaskResponse>(task);
            Assert.IsNotNull(result);

            foreach (var resultMask in result.Results)
            {
                foreach (var maskResult in resultMask)
                {
                    Debug.Log(maskResult.Sequence);
                }
            }
        }

        [Test]
        public async Task Test_02_SummarizationTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("facebook/bart-large-cnn");
            var param = new SummarizationParameters(repetitionPenalty: 1f);
            var task = new SummarizationTask("The tower is 324 metres (1,063 ft) tall, about the same height as an 81-storey building, and the tallest structure in Paris. Its base is square, measuring 125 metres (410 ft) on each side. During its construction, the Eiffel Tower surpassed the Washington Monument to become the tallest man-made structure in the world, a title it held for 41 years until the Chrysler Building in New York City was finished in 1930. It was the first structure to reach a height of 300 metres. Due to the addition of a broadcasting aerial at the top of the tower in 1957, it is now taller than the Chrysler Building by 5.2 metres (17 ft). Excluding transmitters, the Eiffel Tower is the second tallest free-standing structure in France after the Millau Viaduct.", param, model: model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<SummarizationTask, SummarizationResponse>(task);
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
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<QuestionAnsweringTask, QuestionAnsweringResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Debug.Log(result.Result.Answer);
        }

        [Test]
        public async Task Test_04_TableQuestionAnsweringTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("google/tapas-base-finetuned-wtq");
            var tableTestData = new TestTableData(
                repositories: new List<string>
                {
                    "Transformers", "Datasets", "Tokenizers"
                },
                stars: new List<string>
                {
                    "36542", "4512", "3934"
                },
                contributors: new List<string>
                {
                    "651", "77", "34"
                },
                languages: new List<string>
                {
                    "Python", "Python", "Rust, Python and NodeJS",
                });
            var input = new TableQuestionAnsweringInput<TestTableData>("How many stars does the transformers repository have?", tableTestData);
            var task = new TableQuestionAnsweringTask<TestTableData>(input, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<TableQuestionAnsweringTask<TestTableData>, TableQuestionAnsweringResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Debug.Log(result.Result.Answer);
        }

        [Test]
        public async Task Test_05_SentenceSimilarityTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("sentence-transformers/all-MiniLM-L6-v2");
            var input = new SentenceSimilarityInput("That is a happy person", new List<string>
            {
                "That is a happy dog",
                "That is a very happy person",
                "Today is a sunny day"
            });
            var task = new SentenceSimilarityTask(input, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<SentenceSimilarityTask, SentenceSimilarityResponse>(task);
            Assert.IsNotNull(result);

            foreach (var score in result.Scores)
            {
                Debug.Log(score);
            }
        }

        [Test]
        public async Task Test_06_TextClassificationTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("distilbert-base-uncased-finetuned-sst-2-english");
            var input = new List<string>
            {
                "I like you. I love you",
                "I don't like you. I hate you",
            };
            var task = new TextClassificationTask(input, model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<TextClassificationTask, TextClassificationResponse>(task);
            Assert.IsNotNull(result);

            foreach (var classificationResult in result.Results)
            {
                foreach (var textClassificationResult in classificationResult)
                {
                    Debug.Log($"{textClassificationResult.Label}:{textClassificationResult.Score}");
                }
            }
        }

        [Test]
        public async Task Test_07_TextGenerationTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("gpt2");
            var param = new TextGenerationParameters(numberOfResults: 2);
            var task = new TextGenerationTask("The answer to the universe is", param, model: model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<TextGenerationTask, TextGenerationResponse>(task);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.Results);

            foreach (var textGenerationResult in result.Results)
            {
                Debug.Log(textGenerationResult.Text);
            }
        }

        [Test]
        public async Task Test_08_TokenClassificationTask()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.InferenceEndpoint);
            var model = new ModelInfo("dbmdz/bert-large-cased-finetuned-conll03-english");
            var param = new TokenClassificationParameters();
            var task = new TokenClassificationTask("My name is Sarah Jessica Parker but you can call me Jessica", param, model: model);
            var result = await api.InferenceEndpoint.RunInferenceTaskAsync<TokenClassificationTask, TokenClassificationResponse>(task);
            Assert.IsNotNull(result);

            foreach (var resultResult in result.Results)
            {
                foreach (var tokenResult in resultResult)
                {
                    Debug.Log($"{tokenResult.EntityGroup} | {tokenResult.Word} | {tokenResult.Score}");
                }
            }
        }
    }
}
