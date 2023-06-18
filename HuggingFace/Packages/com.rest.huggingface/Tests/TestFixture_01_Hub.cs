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
            var taskList = await api.HubEndpoint.GetAllTasksAsync();
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
                Debug.Log(type.Name);
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
                var widgetCount = taskInfo.WidgetModels.Count;

                if (widgetCount > 0)
                {
                    if (!implementedTasks.TryGetValue(taskId, out var instance))
                    {
                        Debug.LogWarning($"No task implemented for {taskId}!");
                    }
                    else
                    {
                        Debug.Log($"{taskId} | {instance.GetType()} | {string.Join("| ", taskInfo.WidgetModels)}");
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
