using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TokenClassification
{
    public sealed class TokenClassificationTask : BaseJsonPayloadInferenceTask
    {
        public TokenClassificationTask(OneOrMoreOf<string> input, TokenClassificationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("dbmdz/bert-large-cased-finetuned-conll03-english"), options)
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
