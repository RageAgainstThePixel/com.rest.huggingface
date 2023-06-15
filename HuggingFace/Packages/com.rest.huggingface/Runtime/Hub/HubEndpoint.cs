// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
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
            return JsonConvert.DeserializeObject<ModelsTagsByType>(response.Body, client.JsonSerializationOptions);
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
            return JsonConvert.DeserializeObject<IReadOnlyList<ModelInfo>>(response.Body, client.JsonSerializationOptions);
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
            return JsonConvert.DeserializeObject<ModelInfo>(response.Body, client.JsonSerializationOptions);
        }

        public async Task GetRecommendedModel(string task, CancellationToken cancellationToken = default)
        {
            var response = await Rest.GetAsync(GetUrl("/tasks"), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate(true);
        }

        #endregion Models

        /// <summary>
        /// Get username and organizations the user belongs to.
        /// </summary>
        /// <returns><see cref="UserInfo"/></returns>
        public async Task<UserInfo> WhoAmIAsync(CancellationToken cancellationToken = default)
        {
            var response = await Rest.GetAsync(GetUrl("whoami-v2"), parameters: new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate();
            return JsonConvert.DeserializeObject<UserInfo>(response.Body, client.JsonSerializationOptions);
        }
    }
}
