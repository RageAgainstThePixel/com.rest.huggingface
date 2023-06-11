using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HuggingFace.Hub
{
    public class ModelConfig
    {
        [JsonConstructor]
        public ModelConfig(
            [JsonProperty("architectures")] List<string> architectures,
            [JsonProperty("model_type")] string modelType,
            [JsonProperty("task_specific_params")] JToken taskSpecificParams)
        {
            Architectures = architectures;
            ModelType = modelType;
            TaskSpecificParams = taskSpecificParams;
        }

        [JsonProperty("architectures")]
        public IReadOnlyList<string> Architectures { get; }

        [JsonProperty("model_type")]
        public string ModelType { get; }

        [JsonProperty("task_specific_params")]
        public JToken TaskSpecificParams { get; }
    }
}
