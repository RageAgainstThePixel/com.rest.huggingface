using System;
using Utilities.WebRequestRest.Interfaces;

namespace HuggingFace
{
    public class HuggingFaceSettingsInfo : ISettingsInfo
    {
        internal const string DefaultDomain = "huggingface.co";

        public HuggingFaceSettingsInfo()
        {
            Domain = DefaultDomain;
            BaseRequestUrlFormat = $"https://{Domain}/{{0}}";
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
            BaseRequestUrlFormat = $"https://{Domain}/{{0}}";
        }

        public string Domain { get; }

        public string BaseRequestUrlFormat { get; }
    }
}
