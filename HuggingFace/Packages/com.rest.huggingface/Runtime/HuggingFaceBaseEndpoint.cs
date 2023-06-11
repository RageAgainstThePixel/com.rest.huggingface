using Utilities.WebRequestRest;

namespace HuggingFace
{
    public abstract class HuggingFaceBaseEndpoint : BaseEndPoint<HuggingFaceClient, HuggingFaceAuthentication, HuggingFaceSettings>
    {
        protected HuggingFaceBaseEndpoint(HuggingFaceClient client) : base(client) { }

        protected string GetInferenceUrl(string endpoint)
            => string.Format(client.Settings.InferenceRequestUrlFormat, $"{Root}/{endpoint}");
    }
}
