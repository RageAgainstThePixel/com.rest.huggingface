using Newtonsoft.Json;
using System.Collections.Generic;

namespace HuggingFace.Inference.Audio.AudioClassification
{
    public sealed class AudioClassificationResponse : JsonInferenceTaskResponse
    {
        public AudioClassificationResponse(string content, JsonSerializerSettings settings)
            : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<AudioClassificationResults>>(content, settings);
        }

        public IReadOnlyList<AudioClassificationResults> Results { get; }
    }
}
