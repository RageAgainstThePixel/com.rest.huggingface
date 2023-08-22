// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Utilities.WebRequestRest.Interfaces;

namespace HuggingFace
{
    public class HuggingFaceSettingsInfo : ISettingsInfo
    {
        internal const string DefaultDomain = "huggingface.co";
        internal const string InferenceSubDomain = "api-inference";

        public HuggingFaceSettingsInfo()
        {
            Domain = DefaultDomain;
            BaseRequestUrlFormat = $"https://{Domain}/api/{{0}}";
            InferenceRequestUrlFormat = $"https://{InferenceSubDomain}.{Domain}/{{0}}";
        }

        public HuggingFaceSettingsInfo(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                domain = DefaultDomain;
            }

            if (!domain.Contains('.') &&
                !domain.Contains(':'))
            {
                throw new ArgumentException($"Invalid parameter \"{nameof(domain)}\"");
            }

            Domain = domain;
            BaseRequestUrlFormat = $"https://{Domain}/api/{{0}}";
            InferenceRequestUrlFormat = $"https://{InferenceSubDomain}.{Domain}/{{0}}";
        }

        public string Domain { get; }

        public string BaseRequestUrlFormat { get; }

        public string InferenceRequestUrlFormat { get; }
    }
}
