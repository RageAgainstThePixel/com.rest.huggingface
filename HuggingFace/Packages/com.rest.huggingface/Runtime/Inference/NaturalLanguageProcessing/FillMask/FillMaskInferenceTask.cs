using HuggingFace.Hub;
using Newtonsoft.Json;
using System;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskInferenceTask : BaseJsonPayloadInferenceTask
    {
        public FillMaskInferenceTask(string input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("bert-base-uncased"), options)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!string.IsNullOrWhiteSpace(Model?.MaskToken) &&
                !input.Contains(Model.MaskToken))
            {
                throw new InvalidOperationException($"{nameof(input)} is missing mask token: {Model.MaskToken}");
            }

            Input = input;
        }

        public override string TaskId => "fill-mask";

        [JsonProperty("inputs")]
        public string Input { get; }
    }
}
