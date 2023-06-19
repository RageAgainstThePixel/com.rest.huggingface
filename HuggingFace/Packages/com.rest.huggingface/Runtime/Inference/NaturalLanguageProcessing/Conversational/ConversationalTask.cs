// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.Conversational
{
    public sealed class ConversationalTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public ConversationalTask() { }

        public ConversationalTask(Conversation input, ConversationalParameters parameters = null, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("inputs")]
        public Conversation Input { get; }

        [JsonProperty("parameters")]
        public ConversationalParameters Parameters { get; }

        public override string Id => "conversational";
    }
}
