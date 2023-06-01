using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.ZeroShotClassification
{
    public sealed class ZeroShotClassificationTask : BaseJsonPayloadInferenceTask
    {
        public ZeroShotClassificationTask(OneOrMoreOf<string> input, ZeroShotClassificationParameters parameters, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("facebook/bart-large-mnli"), options)
        {
            Input = input.Values;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        [JsonProperty("parameters")]
        public ZeroShotClassificationParameters Parameters { get; }

        public override string TaskId => "zero-shot-classification";
    }
}
