// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.QuestionAnswering
{
    public sealed class QuestionAnsweringTask : BaseJsonPayloadInferenceTask
    {
        [Preserve]
        public QuestionAnsweringTask() { }

        public QuestionAnsweringTask(QuestionAnsweringInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public QuestionAnsweringInput Input { get; }

        public override string Id => "question-answering";
    }
}
