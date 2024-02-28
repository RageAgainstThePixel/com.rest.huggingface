// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using HuggingFace.Inference;
using HuggingFace.Inference.NaturalLanguageProcessing;
using HuggingFace.Inference.NaturalLanguageProcessing.Conversational;
using HuggingFace.Inference.NaturalLanguageProcessing.FillMask;
using HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering;
using HuggingFace.Inference.NaturalLanguageProcessing.SentenceSimilarity;
using HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering;
using HuggingFace.Inference.NaturalLanguageProcessing.TextClassification;
using HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration;
using HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification;
using HuggingFace.Inference.NaturalLanguageProcessing.Translation;
using HuggingFace.Inference.NaturalLanguageProcessing.ZeroShotClassification;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HuggingFace.Tests
{
    /// <summary>
    /// Test class for Accelerated Inference APIs for Natural Language Processing
    /// A list of tasks and their detailed parameters can be found:
    /// https://huggingface.co/docs/api-inference/detailed_parameters#natural-language-processing
    /// </summary>
    internal class TestFixture_02_Inference_NaturalLanguageProcessing : AbstractTestFixture
    {
        [Test]
        public async Task Test_01_FillMaskTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var model = await HuggingFaceClient.HubEndpoint.GetRecommendedModelAsync<FillMaskTask>();
            var task = new FillMaskTask($"The answer to the universe is {model.MaskToken}.", model);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<FillMaskTask, FillMaskResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                foreach (var mask in result)
                {
                    Debug.Log(mask.Sequence);
                }
            }
        }

        [Test]
        public async Task Test_02_SummarizationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var param = new SummarizationParameters(repetitionPenalty: 1f);
            const string input = "The tower is 324 metres (1,063 ft) tall, about the same height as an 81-storey building, and the tallest structure in Paris. Its base is square, measuring 125 metres (410 ft) on each side. During its construction, the Eiffel Tower surpassed the Washington Monument to become the tallest man-made structure in the world, a title it held for 41 years until the Chrysler Building in New York City was finished in 1930. It was the first structure to reach a height of 300 metres. Due to the addition of a broadcasting aerial at the top of the tower in 1957, it is now taller than the Chrysler Building by 5.2 metres (17 ft). Excluding transmitters, the Eiffel Tower is the second tallest free-standing structure in France after the Millau Viaduct.";
            var task = new SummarizationTask(input, param);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<SummarizationTask, SummarizationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var summary in response.Summaries)
            {
                Debug.Log(summary.Text);
            }
        }

        [Test]
        public async Task Test_03_QuestionAnsweringTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var input = new QuestionAnsweringInput("What's my name?", "My name is Clara and I live in Berkeley.");
            var task = new QuestionAnsweringTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<QuestionAnsweringTask, QuestionAnsweringResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Debug.Log(response.Result.Answer);
        }

        [Test]
        public async Task Test_04_TableQuestionAnsweringTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
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
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TableQuestionAnsweringTask<TestTableData>, TableQuestionAnsweringResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Debug.Log(response.Result.Answer);
        }

        [Test]
        public async Task Test_05_SentenceSimilarityTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var input = new SentenceSimilarityInput("That is a happy person", new List<string>
            {
                "That is a happy dog",
                "That is a very happy person",
                "Today is a sunny day"
            });
            var task = new SentenceSimilarityTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<SentenceSimilarityTask, SentenceSimilarityResponse>(task);
            Assert.IsNotNull(response);

            foreach (var score in response.Scores)
            {
                Debug.Log(score);
            }
        }

        [Test]
        public async Task Test_06_TextClassificationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var input = new List<string>
            {
                "I like you. I love you",
                "I don't like you. I hate you",
            };
            var task = new TextClassificationTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TextClassificationTask, TextClassificationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var classificationResult in response.Results)
            {
                foreach (var textClassificationResult in classificationResult)
                {
                    Debug.Log($"{textClassificationResult.Label}:{textClassificationResult.Score}");
                }
            }
        }

        [Test]
        public async Task Test_07_01_TextGenerationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var param = new TextGenerationParameters(topP: 0.9f);
            var task = new TextGenerationTask("The answer to the universe is", param);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TextGenerationTask, TextGenerationResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Debug.Log(result.Text);
            }
        }

        [Test]
        public async Task Test_07_02_TextGenerationTask_Streaming()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var param = new TextGenerationParameters(topP: 0.9f);
            var task = new TextGenerationTask("The answer to the universe is", param, streamCallback: Debug.Log);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TextGenerationTask, TextGenerationResponse>(task);
            Assert.IsNotNull(response);
            Assert.IsNotEmpty(response.Results);

            foreach (var result in response.Results)
            {
                Debug.Log(result.Text);
            }
        }

        [Test]
        public async Task Test_08_TokenClassificationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var param = new TokenClassificationParameters();
            var task = new TokenClassificationTask("My name is Sarah Jessica Parker but you can call me Jessica", param);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TokenClassificationTask, TokenClassificationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var result in response.Results)
            {
                foreach (var token in result)
                {
                    Debug.Log($"{token.EntityGroup} | {token.Word} | {token.Score}");
                }
            }
        }

        [Test]
        public async Task Test_09_TranslationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var input = new List<string>
            {
                "Me llamo Wolfgang y vivo en Berlin",
                "Los ingredientes de una tortilla de patatas son: huevos, patatas y cebolla"
            };
            var task = new TranslationTask(input);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<TranslationTask, TranslationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var translation in response.Results)
            {
                Debug.Log(translation.Text);
            }
        }

        [Test]
        public async Task Test_10_ZeroShotClassificationTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var input = new List<string>
            {
                "I have a problem with my iphone that needs to be resolved asap!"
            };
            var candidateLabels = new List<string>
            {
                "urgent",
                "not urgent",
                "phone",
                "tablet",
                "computer"
            };
            var param = new ZeroShotClassificationParameters(candidateLabels);
            var task = new ZeroShotClassificationTask(input, param);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ZeroShotClassificationTask, ZeroShotClassificationResponse>(task);
            Assert.IsNotNull(response);

            foreach (var zeroShotResult in response.Results)
            {
                Assert.IsTrue(zeroShotResult.Scores.Count == zeroShotResult.Labels.Count);
                var resultCount = zeroShotResult.Scores.Count;

                for (var i = 0; i < resultCount; i++)
                {
                    var label = zeroShotResult.Labels[i];
                    var score = zeroShotResult.Scores[i];
                    Debug.Log($"{label}: {score}");
                }
            }
        }

        [Test]
        public async Task Test_11_ConversationalTask()
        {
            Assert.IsNotNull(HuggingFaceClient.InferenceEndpoint);
            var conversation = new Conversation("Which Movie is the best ?", "It's Die Hard for sure.")
            {
                UserInput = "Can you explain why?"
            };
            var param = new ConversationalParameters(maxLength: 32);
            var task = new ConversationalTask(conversation, param);
            var response = await HuggingFaceClient.InferenceEndpoint.RunInferenceTaskAsync<ConversationalTask, ConversationalResponse>(task);
            Assert.IsNotNull(response);
            Debug.Log(response.Result.Response);
        }
    }
}
