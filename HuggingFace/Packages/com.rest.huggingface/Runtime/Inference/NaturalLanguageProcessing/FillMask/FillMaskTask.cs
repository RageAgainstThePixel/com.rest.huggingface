using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskTask : BaseJsonPayloadInferenceTask
    {
        public FillMaskTask(OneOrMoreOf<string> input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("bert-base-uncased"), options)
        {
            Input = input.Values;
        }

        public override string TaskId => "fill-mask";

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }
    }
}
