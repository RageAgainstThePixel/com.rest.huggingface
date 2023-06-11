// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Utilities.Rest.Extensions;

namespace HuggingFace.Hub
{
    public sealed class ModelSearchArguments
    {
        public enum Direction
        {
            Ascending,
            Descending
        }

        public ModelSearchArguments(
            ModelFilter filter = null,
            string sort = null,
            Direction sortDirection = Direction.Ascending,
            int? limit = null,
            bool? cardData = null,
            bool? fetchConfig = null,
            bool? full = null)
        {
            Filter = filter;
            Sort = sort;
            SortDirection = sortDirection;
            Limit = limit;
            CardData = cardData;
            FetchConfig = fetchConfig;
            Full = full;
        }

        public static implicit operator ModelSearchArguments(string input) => new ModelSearchArguments(new ModelFilter(modelName: input));

        /// <summary>
        /// A string which can be used to identify models on the Hub.
        /// </summary>
        public ModelFilter Filter { get; set; }

        /// <summary>
        /// The key with which to sort the resulting models.<br/>
        /// Possible values: modelId, sha, lastModified, tags, pipeline_tag, siblings, private, author, config, securityStatus
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Direction in which to sort.
        /// </summary>
        public Direction SortDirection { get; set; }

        /// <summary>
        /// The limit on the number of models fetched.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Whether to grab the metadata for the model as well.
        /// Can contain useful information such as carbon emissions, metrics, and datasets trained on.
        /// </summary>
        public bool? CardData { get; set; }

        /// <summary>
        ///  Whether to fetch the model configs as well. This is not included in 'full' due to its size.
        /// </summary>
        public bool? FetchConfig { get; set; }

        public bool? Full { get; set; }

        public override string ToString()
        {
            var @params = new NameValueCollection();

            // Handling author
            if (!string.IsNullOrEmpty(Filter?.Author))
            {
                @params["search"] = $"{Filter.Author}/";
            }

            // Handling model_name
            if (!string.IsNullOrEmpty(Filter?.ModelName))
            {
                @params["search"] += Filter.ModelName;
            }

            // Handling tags
            if (Filter?.Tags is { Count: > 0 })
            {
                @params["tags"] = string.Join(',', Filter.Tags);
            }

            var filterList = new List<string>();

            // Handling tasks
            if (Filter?.Tasks is { Count: > 0 })
            {
                switch (Filter.Tasks.Count)
                {
                    case 1:
                        @params["pipeline_tag"] = Filter.Tasks[0];
                        break;
                    default:
                        filterList.AddRange(Filter.Tasks);
                        break;
                }
            }

            // Handling trained_dataset
            if (Filter?.TrainedDataset is { Count: > 0 })
            {
                const string dataset = "dataset:";
                filterList.AddRange(Filter.TrainedDataset.Select(s => s.Contains(dataset) ? s : $"{dataset}{s}"));
            }

            // Handling library
            if (Filter?.Library is { Count: > 0 })
            {
                filterList.AddRange(Filter.Library);
            }

            if (filterList.Count > 0)
            {
                @params["filter"] = string.Join(',', filterList);
            }

            if (CardData.HasValue)
            {
                @params["cardData"] = CardData.Value.ToString().ToLower();
            }

            if (Filter is { EmissionsThresholds: not null })
            {
                @params["emissions_thresholds"] = $"{Filter.EmissionsThresholds.Value.x},{Filter.EmissionsThresholds.Value.y}";
                @params["cardData"] = "true";
            }

            if (!string.IsNullOrWhiteSpace(Sort))
            {
                @params["sort"] = Sort;
            }

            if (SortDirection == Direction.Descending)
            {
                @params["direction"] = "-1";
            }

            if (Limit.HasValue)
            {
                @params["limit"] = Limit.Value.ToString().ToLower();
            }

            if (Full.HasValue)
            {
                @params["full"] = Full.Value.ToString().ToLower();
            }

            if (FetchConfig.HasValue)
            {
                @params["fetch_config"] = FetchConfig.Value.ToString().ToLower();
            }

            return @params.ToQuery();
        }
    }
}
