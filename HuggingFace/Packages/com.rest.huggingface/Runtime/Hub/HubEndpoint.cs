// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HuggingFace.Inference;
using UnityEngine;
using Utilities.Rest.Extensions;
using Utilities.WebRequestRest;

namespace HuggingFace.Hub
{
    public sealed class HubEndpoint : HuggingFaceBaseEndpoint
    {
        public HubEndpoint(HuggingFaceClient client) : base(client) { }

        protected override string Root => string.Empty;

        #region Models

        /// <summary>
        /// Gets all the available model tags hosted in the Hub
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task<ModelsTagsByType> GetModelTagsAsync(CancellationToken cancellationToken = default)
        {
            var response = await Rest.GetAsync(GetUrl("models-tags-by-type"), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate();
            return JsonConvert.DeserializeObject<ModelsTagsByType>(response.Body, HuggingFaceClient.JsonSerializationOptions);
        }

        /// <summary>
        /// Get information from all models in the Hub.
        /// </summary>
        /// <param name="modelSearchArgs"></param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>List of <see cref="ModelInfo"/>s.</returns>
        public async Task<IReadOnlyList<ModelInfo>> ListModelsAsync(ModelSearchArguments modelSearchArgs = null, CancellationToken cancellationToken = default)
        {
            var uriBuilder = new UriBuilder(GetUrl("models"));

            if (modelSearchArgs != null)
            {
                uriBuilder.Query = modelSearchArgs.ToString();
            }

            var response = await Rest.GetAsync(uriBuilder.Uri.ToString(), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate();
            return JsonConvert.DeserializeObject<IReadOnlyList<ModelInfo>>(response.Body, HuggingFaceClient.JsonSerializationOptions);
        }

        /// <summary>
        /// Get all information for a specific model.
        /// </summary>
        /// <param name="repoId">
        /// A namespace (user or an organization) and a repo name separated by a '/'.
        /// </param>
        /// <param name="revision">
        /// The revision of the model repository from which to get the information.
        /// </param>
        /// <param name="securityStatus">
        /// Whether to retrieve the security status from the model repository as well.
        /// </param>
        /// <param name="filesMetadata">
        /// Whether or not to retrieve metadata for files in the repository (size, LFS metadata, etc). Defaults to 'False'.
        /// </param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns><see cref="ModelInfo"/></returns>
        public async Task<ModelInfo> GetModelDetailsAsync(string repoId, string revision = null, bool securityStatus = false, bool filesMetadata = false, CancellationToken cancellationToken = default)
        {
            var uriBuilder = new UriBuilder(GetUrl(string.IsNullOrWhiteSpace(revision)
                ? $"models/{repoId}"
                : $"models/{repoId}/revision/{Uri.EscapeDataString(revision)}"));

            if (securityStatus || filesMetadata)
            {
                var @params = new NameValueCollection();

                if (securityStatus)
                {
                    @params["securityStatus"] = "true";
                }

                if (filesMetadata)
                {
                    @params["blobs"] = "true";
                }

                uriBuilder.Query = @params.ToQuery();
            }

            var response = await Rest.GetAsync(uriBuilder.Uri.ToString(), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate();
            return JsonConvert.DeserializeObject<ModelInfo>(response.Body, HuggingFaceClient.JsonSerializationOptions);
        }

        /// <summary>
        /// Gets a collection of all the available tasks.
        /// </summary>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="IReadOnlyDictionary{TKey,TValue}"/></returns>
        public async Task<IReadOnlyDictionary<string, TaskInfo>> GetAllTasksAsync(CancellationToken cancellationToken = default)
        {
            var response = await Rest.GetAsync(GetUrl("tasks"), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate();
            return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, TaskInfo>>(response.Body, HuggingFaceClient.JsonSerializationOptions);
        }

        /// <summary>
        /// Gets the recommended model for a given task.
        /// </summary>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>The recommended <see cref="ModelInfo"/> for this task.</returns>
        public async Task<ModelInfo> GetRecommendedModelAsync<T>(CancellationToken cancellationToken = default) where T : InferenceTask
        {
            var task = Activator.CreateInstance<T>();
            return await GetRecommendedModelAsync(task.Id, cancellationToken);
        }

        /// <summary>
        /// Gets the recommended model for a given task.
        /// </summary>
        /// <param name="task">The task to use to get recommended model.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>The recommended <see cref="ModelInfo"/> for this task.</returns>
        public async Task<ModelInfo> GetRecommendedModelAsync(PipelineTag task, CancellationToken cancellationToken = default)
        {
            var tasks = await GetAllTasksAsync(cancellationToken);

            if (tasks.TryGetValue(task, out var taskInfo))
            {
                var modelId = taskInfo.WidgetModels.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(modelId))
                {
                    return await GetModelDetailsAsync(modelId, cancellationToken: cancellationToken);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a collection of recommended models based on search criteria for a given task.
        /// </summary>
        /// <param name="task">The task to use when searching for recommended models.</param>
        /// <param name="sort">Optional, The category to sort by. Default is 'downloads', but other options include by 'likes' and 'modified'.</param>
        /// <param name="limit">Optional, The number of results, Default is 5.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns>A list of <see cref="ModelInfo"/>s.</returns>
        public async Task<IReadOnlyList<ModelInfo>> GetRecommendedModelsAsync(PipelineTag task, string sort = "downloads", int limit = 5, CancellationToken cancellationToken = default)
        {
            var models = await client.HubEndpoint.ListModelsAsync(
                new ModelSearchArguments(
                    new ModelFilter(task: task.ToString()),
                    sort: sort,
                    sortDirection: ModelSearchArguments.Direction.Descending,
                    limit: limit),
                cancellationToken);
            var results = new ConcurrentBag<ModelInfo>();
            var tasks = models.Select(GetModelDetailsTask).ToList();
            await Task.WhenAll(tasks);
            return results.ToList();

            #region locals

            Task GetModelDetailsTask(ModelInfo model)
            {
                async Task GetModelDetailsTaskInternalAsync()
                {
                    try
                    {
                        results.Add(await client.HubEndpoint.GetModelDetailsAsync(model.ModelId, cancellationToken: cancellationToken));
                    }
                    catch (Exception e)
                    {
                        if (e is RestException httpEx &&
                            httpEx.Response.Code == 403)
                        {
                            Debug.LogWarning(httpEx.Message);
                        }
                        else
                        {
                            Debug.LogError(e);
                        }
                    }
                }

                return GetModelDetailsTaskInternalAsync();
            }

            #endregion locals
        }

        #endregion Models

        /// <summary>
        /// Get username and organizations the user belongs to.
        /// </summary>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/></param>
        /// <returns><see cref="UserInfo"/></returns>
        public async Task<UserInfo> WhoAmIAsync(CancellationToken cancellationToken = default)
        {
            var response = await Rest.GetAsync(GetUrl("whoami-v2"), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate();
            return JsonConvert.DeserializeObject<UserInfo>(response.Body, HuggingFaceClient.JsonSerializationOptions);
        }
    }
}
