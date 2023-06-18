// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.FillMask
{
    public sealed class FillMaskTask : BaseJsonPayloadInferenceTask
    {
        internal FillMaskTask() { }

        public FillMaskTask(OneOrMoreOf<string> input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input.Values;
        }

        public override string Id => "fill-mask";

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }
    }
}
