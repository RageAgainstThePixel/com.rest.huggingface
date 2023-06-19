// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Multimodal
{
    public class FeatureExtractionTask : InferenceTask
    {
        [Preserve]
        public FeatureExtractionTask() { }

        public FeatureExtractionTask(FeatureExtractionInput input, ModelInfo model = null, InferenceOptions options = null) : base(model, options)
        {
            Input = input;
        }

        public FeatureExtractionInput Input { get; }

        public override string Id => "feature-extraction";

        public override async Task<string> ToJsonAsync(JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(Input.Text))
            {
                return await Task.FromResult(JsonConvert.SerializeObject(Input, settings));
            }

            var bytes = await ToByteArrayAsync(cancellationToken);
            Input.Text = Convert.ToBase64String(bytes);
            return JsonConvert.SerializeObject(Input, settings);
        }

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(Input.Text))
            {
                return await base.ToByteArrayAsync(cancellationToken);
            }

            await using var memoryStream = new MemoryStream();

            if (Input.Audio != null)
            {
                await Input.Audio.Audio.CopyToAsync(memoryStream, cancellationToken);
            }
            else if (Input.Image != null)
            {
                await Input.Image.Image.CopyToAsync(memoryStream, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Missing valid input to encode!");
            }

            return memoryStream.ToArray();
        }
    }
}
