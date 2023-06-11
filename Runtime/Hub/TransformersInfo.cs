// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace HuggingFace.Hub
{
    public class TransformersInfo
    {
        [JsonConstructor]
        public TransformersInfo(
            [JsonProperty("auto_model")] string autoModel,
            [JsonProperty("pipeline_tag")] string pipelineTag,
            [JsonProperty("processor")] string processor)
        {
            AutoModel = autoModel;
            PipelineTag = pipelineTag;
            Processor = processor;
        }

        [JsonProperty("auto_model")]
        public string AutoModel { get; }

        [JsonProperty("pipeline_tag")]
        public string PipelineTag { get; }

        [JsonProperty("processor")]
        public string Processor { get; }
    }
}
