// Licensed under the MIT License. See LICENSE in the project root for license information.

using HuggingFace.Hub;
using UnityEngine.Scripting;

namespace HuggingFace.Inference.Audio.AudioClassification
{
    public sealed class AudioClassificationTask : BaseAudioInferenceTask
    {
        [Preserve]
        public AudioClassificationTask() { }

        public AudioClassificationTask(SingleSourceAudioInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(input, model, options)
        {
        }

        public override string Id => "audio-classification";
    }
}
