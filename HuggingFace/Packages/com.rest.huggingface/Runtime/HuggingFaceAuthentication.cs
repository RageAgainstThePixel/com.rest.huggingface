// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Utilities.WebRequestRest.Interfaces;

namespace HuggingFace
{
    public sealed class HuggingFaceAuthentication : AbstractAuthentication<HuggingFaceAuthentication, HuggingFaceAuthInfo, HuggingFaceConfiguration>
    {
        internal const string CONFIG_FILE = ".huggingface";
        private const string HUGGING_FACE_API_KEY = nameof(HUGGING_FACE_API_KEY);

        /// <summary>
        /// Allows implicit casting from a string, so that a simple string API key can be provided in place of an instance of Authentication.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public static implicit operator HuggingFaceAuthentication(string apiKey) => new HuggingFaceAuthentication(apiKey);

        /// <summary>
        /// Instantiates an empty Authentication object.
        /// </summary>
        public HuggingFaceAuthentication() { }

        /// <summary>
        /// Instantiates a new Authentication object with the given <paramref name="apiKey"/>, which may be <see langword="null"/>.
        /// </summary>
        /// <param name="apiKey">The API key, required to access the API endpoint.</param>
        public HuggingFaceAuthentication(string apiKey)
        {
            Info = new HuggingFaceAuthInfo(apiKey);
            cachedDefault = this;
        }

        /// <summary>
        /// Instantiates a new Authentication object with the given <paramref name="authInfo"/>, which may be <see langword="null"/>.
        /// </summary>
        /// <param name="authInfo"><see cref="HuggingFaceAuthInfo"/>.</param>
        public HuggingFaceAuthentication(HuggingFaceAuthInfo authInfo)
        {
            Info = authInfo;
            cachedDefault = this;
        }

        /// <summary>
        /// Instantiates a new Authentication object with the given <see cref="configuration"/>.
        /// </summary>
        /// <param name="configuration"><see cref="HuggingFaceConfiguration"/>.</param>
        public HuggingFaceAuthentication(HuggingFaceConfiguration configuration) : this(configuration.ApiKey) { }

        /// <inheritdoc />
        public override HuggingFaceAuthInfo Info { get; }

        private static HuggingFaceAuthentication cachedDefault;

        /// <summary>
        /// The default authentication to use when no other auth is specified.
        /// This can be set manually, or automatically loaded via environment variables or a config file.
        /// <seealso cref="LoadFromEnvironment"/><seealso cref="LoadFromDirectory"/>
        /// </summary>
        public static HuggingFaceAuthentication Default
        {
            get => cachedDefault ??= new HuggingFaceAuthentication().LoadDefault();
            internal set => cachedDefault = value;
        }

        /// <inheritdoc />
        public override HuggingFaceAuthentication LoadFromAsset(HuggingFaceConfiguration configuration = null)
        {
            if (configuration == null)
            {
                Debug.LogWarning($"This can be speed this up by passing a {nameof(HuggingFaceConfiguration)} to the {nameof(HuggingFaceAuthentication)}.ctr");
                configuration = Resources.LoadAll<HuggingFaceConfiguration>(string.Empty).FirstOrDefault(o => o != null);
            }

            return configuration != null ? new HuggingFaceAuthentication(configuration) : null;
        }

        /// <inheritdoc />
        public override HuggingFaceAuthentication LoadFromEnvironment()
        {
            var apiKey = Environment.GetEnvironmentVariable(HUGGING_FACE_API_KEY);
            return string.IsNullOrEmpty(apiKey) ? null : new HuggingFaceAuthentication(apiKey);
        }

        /// <inheritdoc />
        /// ReSharper disable once OptionalParameterHierarchyMismatch
        public override HuggingFaceAuthentication LoadFromDirectory(string directory = null, string filename = CONFIG_FILE, bool searchUp = true)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Environment.CurrentDirectory;
            }

            HuggingFaceAuthInfo tempAuthInfo = null;

            var currentDirectory = new DirectoryInfo(directory);

            while (tempAuthInfo == null && currentDirectory.Parent != null)
            {
                var filePath = Path.Combine(currentDirectory.FullName, filename);

                if (File.Exists(filePath))
                {
                    try
                    {
                        tempAuthInfo = JsonUtility.FromJson<HuggingFaceAuthInfo>(File.ReadAllText(filePath));
                        break;
                    }
                    catch (Exception)
                    {
                        // try to parse the old way for backwards support.
                    }

                    var lines = File.ReadAllLines(filePath);
                    string apiKey = null;

                    foreach (var line in lines)
                    {
                        var parts = line.Split('=', ':');

                        for (var i = 0; i < parts.Length - 1; i++)
                        {
                            var part = parts[i];
                            var nextPart = parts[i + 1];

                            switch (part)
                            {
                                case HUGGING_FACE_API_KEY:
                                    apiKey = nextPart.Trim();
                                    break;
                            }
                        }
                    }

                    tempAuthInfo = new HuggingFaceAuthInfo(apiKey);
                }

                if (searchUp)
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    break;
                }
            }

            if (tempAuthInfo == null ||
                string.IsNullOrEmpty(tempAuthInfo.ApiKey))
            {
                return null;
            }

            return new HuggingFaceAuthentication(tempAuthInfo);
        }
    }
}
