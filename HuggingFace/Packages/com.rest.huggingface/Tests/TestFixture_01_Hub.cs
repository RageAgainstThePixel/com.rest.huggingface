// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;

namespace HuggingFace.Tests
{
    internal class TestFixture_01_Hub
    {
        [Test]
        public async Task Test_01_WhoAmI()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.HubEndpoint);
            var result = await api.HubEndpoint.WhoAmIAsync();
            Assert.IsNotNull(result);
            Debug.Log(result.Fullname);
        }

        [Test]
        public async Task Test_02_Tasks()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.HubEndpoint);
            var tasks = await api.HubEndpoint.GetAllTasksAsync();

            foreach (var (task, taskInfo) in tasks)
            {
                Debug.Log($"{task} | {string.Join("| ", taskInfo.WidgetModels)}");
            }
        }

        [Test]
        public async Task Test_03_Models()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.HubEndpoint);
            var modelTags = await api.HubEndpoint.GetModelTagsAsync();
            Assert.IsNotNull(modelTags);
            Debug.Log($"{modelTags.PipelineTag.Count} pipeline tags");

            foreach (var pipelineTag in modelTags.PipelineTag)
            {
                Debug.Log($"{pipelineTag.Id} | {pipelineTag.Label} | {pipelineTag.SubType}");

                var recommendedModel = await api.HubEndpoint.GetRecommendedModelAsync(pipelineTag);

                if (recommendedModel == null)
                {
                    Debug.LogWarning($"{pipelineTag} does not have a recommended model");
                }

                var recommendedModels = await api.HubEndpoint.GetRecommendedModelsAsync(pipelineTag);

                foreach (var model in recommendedModels)
                {
                    Debug.Log($"-->  {model} | Likes: {model.Likes}");

                    if (!string.IsNullOrWhiteSpace(model.Config?.TaskSpecificParams?.ToString()))
                    {
                        Debug.Log($"        task-specific-params: {model.Config.TaskSpecificParams}");
                    }
                }
            }
        }
    }
}
