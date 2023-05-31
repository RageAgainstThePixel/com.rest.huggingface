using HuggingFace;
using HuggingFace.Hub;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;

namespace Rest.HuggingFace.Tests
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

                var filter = new ModelFilter(task: pipelineTag.ToString());
                var args = new ModelSearchArguments(filter, limit: 1, cardData: true, fetchConfig: true);
                var models = await api.HubEndpoint.ListModelsAsync(args);

                foreach (var model in models)
                {
                    Debug.Log($"{model.ModelId} | Downloads: {model.Downloads} | PipelineTag: {model.PipelineTag}");
                    var modelDetails = await api.HubEndpoint.GetModelDetailsAsync(model.ModelId);
                    Assert.IsNotNull(modelDetails);

                    if (!string.IsNullOrWhiteSpace(modelDetails.MaskToken))
                    {
                        Debug.LogWarning($"{modelDetails.MaskToken}");
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
