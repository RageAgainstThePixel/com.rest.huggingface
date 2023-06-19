// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.NaturalLanguageProcessing.TableQuestionAnswering
{
    public sealed class TableQuestionAnsweringTask<TTableData> : BaseJsonPayloadInferenceTask
        where TTableData : class
    {
        [Preserve]
        public TableQuestionAnsweringTask() { }

        public TableQuestionAnsweringTask(TableQuestionAnsweringInput<TTableData> input, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
        }

        [JsonProperty("inputs")]
        public TableQuestionAnsweringInput<TTableData> Input { get; }

        public override string Id => "table-question-answering";
    }
}
