// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing
{
    public sealed class SummarizationTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public SummarizationTask() { }

        public override string Id => "summarization";

        public SummarizationTask(OneOrMoreOf<string> input, SummarizationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input.Values;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        [JsonProperty("parameters")]
        public SummarizationParameters Parameters { get; }
    }
}
