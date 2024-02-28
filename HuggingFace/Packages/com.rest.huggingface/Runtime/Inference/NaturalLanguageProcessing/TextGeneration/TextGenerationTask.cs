// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    /// <summary>
    /// https://huggingface.github.io/text-generation-inference/
    /// </summary>
    public sealed class TextGenerationTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public TextGenerationTask() { }

        public TextGenerationTask(string input, TextGenerationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null, Action<string> streamCallback = null)
            : base(model, options, streamCallback)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public string Input { get; }

        [JsonProperty("parameters")]
        public TextGenerationParameters Parameters { get; }

        public override string Id => "text-generation";
    }
}
