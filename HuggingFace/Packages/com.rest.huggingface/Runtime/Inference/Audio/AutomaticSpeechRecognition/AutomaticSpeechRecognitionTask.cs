using HuggingFace.Hub;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HuggingFace.Inference.Audio
{
    public sealed class AutomaticSpeechRecognitionTask : InferenceTask
    {
        public AutomaticSpeechRecognitionTask(AutomaticSpeechRecognitionInput input, ModelInfo model = null, InferenceOptions options = null)
            : base(model ?? new ModelInfo("facebook/wav2vec2-base-960h"), options)
        {
            Input = input;
        }

        public AutomaticSpeechRecognitionInput Input { get; }

        public override string Id => "automatic-speech-recognition";

        public override async Task<byte[]> ToByteArrayAsync(CancellationToken cancellationToken = default)
        {
            await using var memoryStream = new MemoryStream();
            await Input.Audio.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
