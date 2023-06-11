using Newtonsoft.Json;
using UnityEngine;

namespace HuggingFace.Inference.Audio.AudioToAudio
{
    public sealed class AudioToAudioResult
    {
        [JsonConstructor]
        public AudioToAudioResult(
            [JsonProperty("label")] string label,
            [JsonProperty("blob")] string blob)
        {
            Label = label;
            Blob = blob;
        }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("blob")]
        public string Blob { get; }

        [JsonIgnore]
        public AudioClip AudioClip { get; internal set; }
    }
}
