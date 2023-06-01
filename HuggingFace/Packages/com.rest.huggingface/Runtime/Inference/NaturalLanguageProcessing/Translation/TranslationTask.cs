using System.Collections.Generic;
using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Translation
{
    public sealed class TranslationTask : BaseJsonPayloadInferenceTask
    {
        public TranslationTask(OneOrMoreOf<string> input, ModelInfo model, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input.Values;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        public override string TaskId => "translation";
    }
}
