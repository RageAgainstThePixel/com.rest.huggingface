using HuggingFace.Hub;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextClassification
{
    public sealed class TextClassificationTask : BaseJsonPayloadInferenceTask
    {
        public TextClassificationTask(OneOrMoreOf<string> input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("distilbert-base-uncased-finetuned-sst-2-english"), options)
        {
            Input = input.Values;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        public override string Id => "text-classification";
    }
}
