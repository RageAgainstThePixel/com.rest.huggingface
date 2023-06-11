using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.SentenceSimilarity
{
    public sealed class SentenceSimilarityTask : BaseJsonPayloadInferenceTask
    {
        public SentenceSimilarityTask(SentenceSimilarityInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("sentence-transformers/all-MiniLM-L6-v2"), options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public SentenceSimilarityInput Input { get; }

        public override string Id => "sentence-similarity";
    }
}
