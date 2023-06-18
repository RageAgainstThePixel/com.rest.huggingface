// Licensed under the MIT License. See LICENSE in the project root for license information.

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
