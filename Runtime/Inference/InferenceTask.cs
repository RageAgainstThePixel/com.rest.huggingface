// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HuggingFace.Inference
{
    public abstract class InferenceTask
    {
        protected InferenceTask() { }

        protected InferenceTask(ModelInfo model, InferenceOptions options)
        {
            Model = model;
            Options = options ?? new InferenceOptions();
        }

        [JsonIgnore]
        public abstract string Id { get; }

        [JsonIgnore]
        public ModelInfo Model { get; internal set; }

        [JsonProperty("options")]
        public InferenceOptions Options { get; }

        public virtual Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
            => Task.FromResult(string.Empty);

        public virtual async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken)
            => await Task.FromResult(Array.Empty<byte>());

        public override string ToString() => Id;

        public static implicit operator string(InferenceTask task) => task.ToString();
    }
}
