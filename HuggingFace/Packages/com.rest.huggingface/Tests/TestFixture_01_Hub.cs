using NUnit.Framework;
using System.Linq;
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
        public async Task Test_02_Models()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.HubEndpoint);
            var modelTags = await api.HubEndpoint.GetModelTagsAsync();
            Assert.IsNotNull(modelTags);
            Debug.Log($"{modelTags.PipelineTag.Count} pipeline tags");

            foreach (var pipelineTag in modelTags.PipelineTag)
            {
                Debug.Log($"{pipelineTag.Id} | {pipelineTag.Label} | {pipelineTag.SubType}");

                var recommendedModels = await api.InferenceEndpoint.GetRecommendedModelsAsync(pipelineTag);
                var recommendedModel = recommendedModels.FirstOrDefault(info => !info.Private && !info.Disabled);
                Assert.IsNotNull(recommendedModel);

                foreach (var model in recommendedModels)
                {
                    Debug.Log($"-->  {model.ModelId} | Likes: {model.Likes}");

                    if (!string.IsNullOrWhiteSpace(model.Config?.TaskSpecificParams?.ToString()))
                    {
                        Debug.Log($"        task-specific-params: {model.Config.TaskSpecificParams}");
                    }
                }
            }
        }

        [Test]
        public async Task Test_03_Datasets()
        {
            var api = new HuggingFaceClient();
            Assert.IsNotNull(api.HubEndpoint);
            var datasetTags = await api.HubEndpoint.GetDatasetTagsAsync();
            Assert.IsNotNull(datasetTags);
            Debug.Log($"{datasetTags.TaskIds.Count} Task Categories");

            // await api.HubEndpoint.ListDatasetsAsync();
            // await api.HubEndpoint.GetDatasetDetailsAsync("");
        }

        [Test]
        public async Task Test_04_Spaces()
        {
            //var api = new HuggingFaceClient();
            //Assert.IsNotNull(api.HubEndpoint);
            await Task.CompletedTask;
            // await api.HubEndpoint.ListSpacesAsync();
            // await api.HubEndpoint.GetSpaceDetailsAsync("");
        }
    }
}
