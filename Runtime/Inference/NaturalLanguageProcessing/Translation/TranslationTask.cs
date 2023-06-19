// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Translation
{
    public sealed class TranslationTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public TranslationTask() { }

        public TranslationTask(OneOrMoreOf<string> input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input.Values;
        }

        [JsonProperty("inputs")]
        public IReadOnlyList<string> Input { get; }

        public override string Id => "translation";
    }
}
