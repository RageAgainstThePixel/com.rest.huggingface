// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Inference;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace HuggingFace.Tests
{
    internal class TestFixture_01_Hub : AbstractTestFixture
    {
        [Test]
        public async Task Test_01_WhoAmI()
        {
            Assert.IsNotNull(HuggingFaceClient.HubEndpoint);
            var result = await HuggingFaceClient.HubEndpoint.WhoAmIAsync();
            Assert.IsNotNull(result);
            Debug.Log(result.Fullname);
        }

        [Test]
        public async Task Test_02_Tasks()
        {
            Assert.IsNotNull(HuggingFaceClient.HubEndpoint);
            var taskList = await HuggingFaceClient.HubEndpoint.GetAllTasksAsync();
            var implementedTasks = new Dictionary<string, InferenceTask>();

            foreach (var type in
                     from type in AppDomain.CurrentDomain.GetAssemblies()
                         .SelectMany(assembly => assembly.GetTypes())
                         .Where(type => type.IsClass &&
                                        !type.IsAbstract &&
                                        !type.IsInterface &&
                                        typeof(InferenceTask).IsAssignableFrom(type))
                     select type)
            {
                InferenceTask instance;

                try
                {
                    if (type.ContainsGenericParameters)
                    {
                        var genericType = type.MakeGenericType(typeof(object));
                        instance = Activator.CreateInstance(genericType) as InferenceTask;
                    }
                    else
                    {
                        instance = Activator.CreateInstance(type) as InferenceTask;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    continue;
                }

                if (instance is { } task)
                {
                    if (!implementedTasks.TryAdd(task.Id, task))
                    {
                        Debug.LogError($"Failed to insert instance for{type.Name}");
                    }
                }
                else
                {
                    Debug.LogError($"Failed to create an instance for {type.Name}");
                }
            }

            foreach (var (taskId, taskInfo) in taskList)
            {
                var recommendedModel = await HuggingFaceClient.HubEndpoint.GetRecommendedModelAsync(taskId);

                if (recommendedModel != null)
                {
                    if (!implementedTasks.TryGetValue(taskId, out var instance))
                    {
                        Debug.LogWarning($"No task implemented for {taskId}!");
                    }
                    else
                    {
                        Debug.Log($"{taskId} | {instance.GetType()} | {recommendedModel}");
                    }
                }
                else
                {
                    Debug.Log($"{taskId} has no widgets or tasks");
                }
            }
        }

        [Test]
        public async Task Test_03_Models()
        {
            Assert.IsNotNull(HuggingFaceClient.HubEndpoint);
            var modelTags = await HuggingFaceClient.HubEndpoint.GetModelTagsAsync();
            Assert.IsNotNull(modelTags);
            Debug.Log($"{modelTags.PipelineTag.Count} pipeline tags");

            foreach (var pipelineTag in modelTags.PipelineTag)
            {
                Debug.Log($"{pipelineTag.Id} | {pipelineTag.Label} | {pipelineTag.SubType}");

                var recommendedModel = await HuggingFaceClient.HubEndpoint.GetRecommendedModelAsync(pipelineTag);

                if (recommendedModel == null)
                {
                    Debug.LogWarning($"{pipelineTag} does not have a recommended model");
                }

                var recommendedModels = await HuggingFaceClient.HubEndpoint.GetRecommendedModelsAsync(pipelineTag);

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
