using HuggingFace.Hub;
using HuggingFace.Inference;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Authentication;
using Utilities.WebRequestRest;

namespace HuggingFace
{
    public class HuggingFaceClient : BaseClient<HuggingFaceAuthentication, HuggingFaceSettings>
    {
        /// <summary>
        /// Creates a new client for the Eleven Labs API, handling auth and allowing for access to various API endpoints.
        /// </summary>
        /// <param name="authentication">The API authentication information to use for API calls,
        /// or <see langword="null"/> to attempt to use the <see cref="HuggingFaceAuthentication.Default"/>,
        /// potentially loading from environment vars or from a config file.</param>
        /// <param name="settings">Optional, <see cref="HuggingFaceSettings"/> for specifying a proxy domain.</param>
        /// <exception cref="AuthenticationException">Raised when authentication details are missing or invalid.</exception>
        public HuggingFaceClient(HuggingFaceAuthentication authentication = null, HuggingFaceSettings settings = null)
            : base(authentication ?? HuggingFaceAuthentication.Default, settings ?? HuggingFaceSettings.Default)
        {
            JsonSerializationOptions = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            };

            HubEndpoint = new HubEndpoint(this);
            InferenceEndpoint = new InferenceEndpoint(this);
        }


        protected override void ValidateAuthentication()
        {
            if (!HasValidAuthentication)
            {
                throw new AuthenticationException("You must provide API authentication.  Please refer to https://github.com/RageAgainstThePixel/com.rest.huggingface#authentication for details.");
            }
        }

        protected override void SetupDefaultRequestHeaders()
            => DefaultRequestHeaders = new Dictionary<string, string>
            {
#if !UNITY_WEBGL
                {"User-Agent", "com.rest.huggingface" },
#endif
                {"Authorization", Rest.GetBearerOAuthToken(Authentication.Info.ApiKey) }
            };

        public override bool HasValidAuthentication => !string.IsNullOrWhiteSpace(Authentication?.Info?.ApiKey);

        internal JsonSerializerSettings JsonSerializationOptions { get; }

        public HubEndpoint HubEndpoint { get; }

        public InferenceEndpoint InferenceEndpoint { get; }
    }
}
