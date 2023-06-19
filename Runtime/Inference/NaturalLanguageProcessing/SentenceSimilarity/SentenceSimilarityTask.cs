// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.SentenceSimilarity
{
    public sealed class SentenceSimilarityTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public SentenceSimilarityTask() { }

        public SentenceSimilarityTask(SentenceSimilarityInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public SentenceSimilarityInput Input { get; }

        public override string Id => "sentence-similarity";
    }
}
