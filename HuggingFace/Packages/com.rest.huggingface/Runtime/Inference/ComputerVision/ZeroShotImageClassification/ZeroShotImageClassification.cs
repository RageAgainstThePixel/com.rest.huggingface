// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.ComputerVision.ZeroShotImageClassification
{
    public sealed class ZeroShotImageClassificationTask : InferenceTask
    {
        [Preserve]
        public ZeroShotImageClassificationTask() { }

        public ZeroShotImageClassificationTask(SingleSourceImageInput input, ZeroShotClassificationParameters parameters, ModelInfo model = null, InferenceOptions options = null)
            : base(model, options)
        {
            Input = input;
            Parameters = parameters;
        }

        [JsonProperty("image")]
        public string Image { get; internal set; }

        [JsonProperty("parameters")]
        public ZeroShotClassificationParameters Parameters { get; }

        [JsonIgnore]
        public SingleSourceImageInput Input { get; }

        public override string Id => "zero-shot-image-classification";

        public override async Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            var bytes = await ToByteArrayAsync(cancellationToken);
            Image = Convert.ToBase64String(bytes);
            return JsonConvert.SerializeObject(this, settings);
        }

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await Input.Image.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }

    public sealed class ZeroShotImageClassificationResponse : JsonInferenceTaskResponse
    {
        public ZeroShotImageClassificationResponse(string content, JsonSerializerSettings settings) : base(content, settings)
        {
            Results = JsonConvert.DeserializeObject<IReadOnlyList<ScoreResults>>(content, settings);
        }

        public IReadOnlyList<ScoreResults> Results { get; }
    }
}
