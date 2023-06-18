// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.ZeroShotClassification
{
    public sealed class ZeroShotClassificationTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public ZeroShotClassificationTask() { }

        public ZeroShotClassificationTask(OneOrMoreOf<string> input, ZeroShotClassificationParameters parameters, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input.Values;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        [JsonProperty("parameters")]
        public ZeroShotClassificationParameters Parameters { get; }

        public override string Id => "zero-shot-classification";
    }
}
