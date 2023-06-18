// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TextGeneration
{
    public sealed class TextGenerationTask : BaseJsonPayloadInferenceTask
    {
        internal TextGenerationTask() { }

        public TextGenerationTask(string input, TextGenerationParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
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
