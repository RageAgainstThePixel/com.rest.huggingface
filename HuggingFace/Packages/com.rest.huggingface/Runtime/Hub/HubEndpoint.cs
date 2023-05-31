// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Rest.Extensions;

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
            var response = await client.Client.GetAsync(GetUrl("models-tags-by-type"), cancellationToken);
            var responseAsString = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelsTagsByType>(responseAsString, client.JsonSerializationOptions);
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

            var response = await client.Client.GetAsync(uriBuilder.Uri, cancellationToken);
            var responseAsString = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IReadOnlyList<ModelInfo>>(responseAsString, client.JsonSerializationOptions);
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

            var response = await client.Client.GetAsync(uriBuilder.Uri, cancellationToken);
            var responseAsString = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelInfo>(responseAsString, client.JsonSerializationOptions);
        }

        #endregion Models

        #region Datasets

        /// <summary>
        /// Gets all the available dataset tags hosted in the Hub
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task<DatasetsTagsByType> GetDatasetTagsAsync(CancellationToken cancellationToken = default)
        {
            var response = await client.Client.GetAsync(GetUrl("datasets-tags-by-type"), cancellationToken);
            var responseAsString = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DatasetsTagsByType>(responseAsString, client.JsonSerializationOptions);
        }

        /// <summary>
        /// Get information from all datasets in the Hub.
        /// </summary>
        /// <param name="datasetSearchArgs"></param>
        /// <param name="cancellationToken"></param>
        public Task ListDatasetsAsync(DataSetSearchArguments datasetSearchArgs = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            //var uriBuilder = new UriBuilder(GetUrl("datasets"));

            //if (datasetSearchArgs != null)
            //{
            //    uriBuilder.Query = datasetSearchArgs.ToString();
            //}

            //var response = await client.Client.GetAsync(uriBuilder.Uri, cancellationToken);
            //var responseAsString = await response.ReadAsStringAsync(true);
            //return responseAsString;
        }

        /// <summary>
        /// Get all information for a specific dataset.
        /// - full: Whether to fetch most dataset data, such as all tags, the files, etc.
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="revision"></param>
        /// <param name="filesMetadata"></param>
        /// <param name="cancellationToken"></param>
        public Task GetDatasetDetailsAsync(string repoId, string revision = null, bool filesMetadata = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            //var uriBuilder = new UriBuilder(GetUrl(string.IsNullOrWhiteSpace(revision)
            //    ? $"datasets/{repoId}"
            //    : $"datasets/{repoId}/revision/{Uri.EscapeDataString(revision)}"));

            //if (filesMetadata)
            //{
            //    uriBuilder.Query = "blobs=true";
            //}

            //Debug.Log(uriBuilder.Uri);
            //await Task.CompletedTask;
        }

        #endregion Datasets

        #region Spaces

        /// <summary>
        /// Get information from all Spaces in the Hub.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task ListSpacesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            //var uriBuilder = new UriBuilder(GetUrl("spaces"));
            //var response = await client.Client.GetAsync(uriBuilder.Uri, cancellationToken);
            //var responseAsString = await response.ReadAsStringAsync(true);
            //return responseAsString;
        }

        /// <summary>
        /// Get all information for a specific space.
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="revision"></param>
        /// <param name="filesMetadata"></param>
        /// <param name="cancellationToken"></param>
        public Task GetSpaceDetailsAsync(string repoId, string revision = null, bool filesMetadata = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            //var uriBuilder = new UriBuilder(GetUrl(string.IsNullOrWhiteSpace(revision)
            //    ? $"spaces/{repoId}"
            //    : $"spaces/{repoId}/revision/{Uri.EscapeDataString(revision)}"));

            //if (filesMetadata)
            //{
            //    uriBuilder.Query = "blobs=true";
            //}

            //Debug.Log(uriBuilder.Uri);
            //await Task.CompletedTask;
        }

        #endregion Spaces

        /// <summary>
        /// Get username and organizations the user belongs to.
        /// </summary>
        /// <returns><see cref="UserInfo"/></returns>
        public async Task<UserInfo> WhoAmIAsync(CancellationToken cancellationToken = default)
        {
            var response = await client.Client.GetAsync(GetUrl("whoami-v2"), cancellationToken);
            var responseAsString = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserInfo>(responseAsString, client.JsonSerializationOptions);
        }
    }
}
