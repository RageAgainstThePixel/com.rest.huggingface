using UnityEngine;
using Utilities.WebRequestRest.Interfaces;

namespace HuggingFace
{
    [CreateAssetMenu(fileName = nameof(HuggingFaceConfiguration), menuName = nameof(HuggingFace) + "/" + nameof(HuggingFaceConfiguration), order = 0)]
    public class HuggingFaceConfiguration : ScriptableObject, IConfiguration
    {
        [SerializeField]
        [Tooltip("The api key.")]
        private string apiKey;

        public string ApiKey => apiKey;

        [SerializeField]
        [Tooltip("Optional proxy domain to make requests though.")]
        private string proxyDomain;

        public string ProxyDomain => proxyDomain;
    }
}
