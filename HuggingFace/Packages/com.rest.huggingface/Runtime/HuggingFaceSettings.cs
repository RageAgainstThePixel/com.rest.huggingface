using System.Linq;
using UnityEngine;
using Utilities.WebRequestRest.Interfaces;

namespace HuggingFace
{
    public class HuggingFaceSettings : ISettings<HuggingFaceSettingsInfo>
    {
        public HuggingFaceSettings()
        {
            if (cachedDefault != null) { return; }

            var config = Resources.LoadAll<HuggingFaceConfiguration>(string.Empty)
                .FirstOrDefault(asset => asset != null);

            if (config != null)
            {
                Info = new HuggingFaceSettingsInfo(config.ProxyDomain);
                cachedDefault = new HuggingFaceSettings(Info);
            }
            else
            {
                Info = new HuggingFaceSettingsInfo();
                cachedDefault = new HuggingFaceSettings(Info);
            }
        }

        public HuggingFaceSettings(HuggingFaceSettingsInfo settingsInfo)
            => Info = settingsInfo;

        public HuggingFaceSettings(string domain)
            => Info = new HuggingFaceSettingsInfo(domain);

        private static HuggingFaceSettings cachedDefault;

        public static HuggingFaceSettings Default
        {
            get => cachedDefault ?? new HuggingFaceSettings();
            internal set => cachedDefault = value;
        }

        public HuggingFaceSettingsInfo Info { get; }

        public string BaseRequestUrlFormat => Info.BaseRequestUrlFormat;

        public string InferenceRequestUrlFormat => Info.InferenceRequestUrlFormat;
    }
}
