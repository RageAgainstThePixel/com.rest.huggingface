// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification
{
    public sealed class TokenClassificationTask : BaseJsonPayloadInferenceTask
    {
        internal TokenClassificationTask() { }

        public TokenClassificationTask(OneOrMoreOf<string> input, TokenClassificationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input.Values;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        [JsonProperty("parameters")]
        public TokenClassificationParameters Parameters { get; }

        public override string Id => "token-classification";
    }
}
