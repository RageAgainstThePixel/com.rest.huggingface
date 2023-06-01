using System.Collections.Generic;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.ZeroShotClassification
{
    public sealed class ZeroShotClassificationParameters
    {
        public ZeroShotClassificationParameters(IReadOnlyList<string> candidateLabels, bool multiLabel = false)
        {
            CandidateLabels = candidateLabels;
            MultiLabel = multiLabel;
        }

        /// <summary>
        /// A list of strings that are potential classes for inputs. (max 10 candidate_labels, for more, simply run multiple requests, results are going to be misleading if using too many candidate_labels anyway. If you want to keep the exact same, you can simply run multi_label=True and do the scaling on your end.)
        /// </summary>
        [JsonProperty("candidate_labels")]
        public IReadOnlyList<string> CandidateLabels { get; set; }

        /// <summary>
        /// (Default: false) Boolean that is set to True if classes can overlap
        /// </summary>
        [JsonProperty("multi_label")]
        public bool MultiLabel { get; set; }
    }
}
