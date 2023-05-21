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
            Default = config != null
                ? new HuggingFaceSettings(new HuggingFaceSettingsInfo(config.ProxyDomain))
                : new HuggingFaceSettings(new HuggingFaceSettingsInfo());
        }

        public HuggingFaceSettings(HuggingFaceSettingsInfo settingsInfo)
            => this.settingsInfo = settingsInfo;

        public HuggingFaceSettings(string domain)
            => settingsInfo = new HuggingFaceSettingsInfo(domain);

        private static HuggingFaceSettings cachedDefault;

        public static HuggingFaceSettings Default
        {
            get => cachedDefault ?? new HuggingFaceSettings();
            internal set => cachedDefault = value;
        }

        private readonly HuggingFaceSettingsInfo settingsInfo;

        public HuggingFaceSettingsInfo Info => settingsInfo ?? Default.Info;

        public string BaseRequestUrlFormat => Info.BaseRequestUrlFormat;
    }
}
