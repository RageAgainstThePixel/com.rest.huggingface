using HuggingFace.Hub;
using Newtonsoft.Json;
using System;

namespace HuggingFace.Inference
{
    public abstract class InferenceTask
    {
        protected InferenceTask(ModelInfo model, InferenceOptions options)
        {
            if (model == null ||
                string.IsNullOrWhiteSpace(model.ModelId))
            {
                throw new ArgumentNullException(nameof(model));
            }

            Model = model;
            Options = options ?? new InferenceOptions();
        }

        [JsonIgnore]
        public abstract string Id { get; }

        [JsonIgnore]
        public ModelInfo Model { get; }

        [JsonProperty("options")]
        public InferenceOptions Options { get; }

        public virtual string ToJson(JsonSerializerSettings settings)
            => string.Empty;

        public virtual byte[] ToByteArray()
            => Array.Empty<byte>();

        public override string ToString() => Id;

        public static implicit operator string(InferenceTask task) => task.ToString();
    }
}
