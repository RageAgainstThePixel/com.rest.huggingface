// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using UnityEngine;
using Utilities.WebRequestRest.Interfaces;

namespace HuggingFace
{
    public class HuggingFaceSettings : ISettings<HuggingFaceSettingsInfo>
    {
        public HuggingFaceSettings()
        {
            Info = new HuggingFaceSettingsInfo();
            cachedDefault = new HuggingFaceSettings(Info);
        }

        public HuggingFaceSettings(HuggingFaceConfiguration configuration)
        {
            if (configuration == null)
            {
                Debug.LogWarning($"You can speed this up by passing a {nameof(HuggingFaceConfiguration)} to the {nameof(HuggingFaceSettings)}.ctr");
                configuration = Resources.LoadAll<HuggingFaceConfiguration>(string.Empty).FirstOrDefault(asset => asset != null);
            }

            if (configuration == null)
            {
                throw new MissingReferenceException($"Failed to find a valid {nameof(HuggingFaceConfiguration)}!");
            }

            if (configuration != null)
            {
                Info = new HuggingFaceSettingsInfo(configuration.ProxyDomain);
                cachedDefault = new HuggingFaceSettings(Info);
            }
        }

        public HuggingFaceSettings(HuggingFaceSettingsInfo settingsInfo)
        {
            Info = settingsInfo;
            cachedDefault = this;
        }

        public HuggingFaceSettings(string domain)
        {
            Info = new HuggingFaceSettingsInfo(domain);
            cachedDefault = this;
        }

        private static HuggingFaceSettings cachedDefault;

        public static HuggingFaceSettings Default
        {
            get => cachedDefault ?? new HuggingFaceSettings(configuration: null);
            internal set => cachedDefault = value;
        }

        public HuggingFaceSettingsInfo Info { get; }

        public string BaseRequestUrlFormat => Info.BaseRequestUrlFormat;

        public string InferenceRequestUrlFormat => Info.InferenceRequestUrlFormat;
    }
}
