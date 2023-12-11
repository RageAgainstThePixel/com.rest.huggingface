// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;
using System;
using System.IO;
using System.Security.Authentication;
using UnityEditor;
using UnityEngine;

namespace HuggingFace.Tests
{
    internal class TestFixture_00_Authentication
    {
        [SetUp]
        public void Setup()
        {
            var authJson = new HuggingFaceAuthInfo("key-test12");
            var authText = JsonUtility.ToJson(authJson, true);
            File.WriteAllText(HuggingFaceAuthentication.CONFIG_FILE, authText);
        }

        [Test]
        public void Test_01_GetAuthFromEnv()
        {
            var auth = new HuggingFaceAuthentication().LoadFromEnvironment();
            Assert.IsNotNull(auth);
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.IsNotEmpty(auth.Info.ApiKey);
        }

        [Test]
        public void Test_02_GetAuthFromFile()
        {
            var auth = new HuggingFaceAuthentication().LoadFromPath(Path.GetFullPath(HuggingFaceAuthentication.CONFIG_FILE));
            Assert.IsNotNull(auth);
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.AreEqual("key-test12", auth.Info.ApiKey);
        }

        [Test]
        public void Test_03_GetAuthFromNonExistentFile()
        {
            var auth = new HuggingFaceAuthentication().LoadFromDirectory(filename: "bad.config");
            Assert.IsNull(auth);
        }

        [Test]
        public void Test_04_GetAuthFromConfiguration()
        {
            var configPath = $"Assets/Resources/{nameof(HuggingFaceConfiguration)}.asset";
            var cleanup = false;

            if (!File.Exists(Path.GetFullPath(configPath)))
            {
                if (!Directory.Exists($"{Application.dataPath}/Resources"))
                {
                    Directory.CreateDirectory($"{Application.dataPath}/Resources");
                }

                var instance = ScriptableObject.CreateInstance<HuggingFaceConfiguration>();
                instance.ApiKey = "key-test12";
                AssetDatabase.CreateAsset(instance, configPath);
                cleanup = true;
            }

            var configuration = AssetDatabase.LoadAssetAtPath<HuggingFaceConfiguration>(configPath);
            Assert.IsNotNull(configuration);
            var auth = new HuggingFaceAuthentication().LoadFromAsset(configuration);

            Assert.IsNotNull(auth);
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.IsNotEmpty(auth.Info.ApiKey);
            Assert.AreEqual(auth.Info.ApiKey, configuration.ApiKey);

            if (cleanup)
            {
                AssetDatabase.DeleteAsset(configPath);
                AssetDatabase.DeleteAsset("Assets/Resources");
            }
        }

        [Test]
        public void Test_05_Authentication()
        {
            var defaultAuth = HuggingFaceAuthentication.Default;

            Assert.IsNotNull(defaultAuth);
            Assert.IsNotNull(defaultAuth);
            Assert.IsNotNull(defaultAuth.Info.ApiKey);
            Assert.AreEqual(defaultAuth.Info.ApiKey, HuggingFaceAuthentication.Default.Info.ApiKey);

            var manualAuth = new HuggingFaceAuthentication("key-testAA");
            Assert.IsNotNull(manualAuth);
            Assert.IsNotNull(manualAuth.Info);
            Assert.IsNotNull(manualAuth.Info.ApiKey);
            Assert.AreEqual(manualAuth.Info.ApiKey, HuggingFaceAuthentication.Default.Info.ApiKey);

            HuggingFaceAuthentication.Default = defaultAuth;
        }

        [Test]
        public void Test_06_GetKey()
        {
            var auth = new HuggingFaceAuthentication("key-testAA");
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.AreEqual("key-testAA", auth.Info.ApiKey);
        }

        [Test]
        public void Test_07_GetKeyFailed()
        {
            HuggingFaceAuthentication auth = null;

            try
            {
                auth = new HuggingFaceAuthentication("fail-key");
            }
            catch (InvalidCredentialException)
            {
                Assert.IsNull(auth);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false, $"Expected exception {nameof(InvalidCredentialException)} but got {e.GetType().Name}");
            }
        }

        [Test]
        public void Test_08_ParseKey()
        {
            var auth = new HuggingFaceAuthentication("key-testAA");
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.AreEqual("key-testAA", auth.Info.ApiKey);
            auth = "key-testCC";
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.AreEqual("key-testCC", auth.Info.ApiKey);

            auth = new HuggingFaceAuthentication("key-testBB");
            Assert.IsNotNull(auth.Info.ApiKey);
            Assert.AreEqual("key-testBB", auth.Info.ApiKey);
        }

        [Test]
        public void Test_12_CustomDomainConfigurationSettings()
        {
            var auth = new HuggingFaceAuthentication("customIssuedToken");
            var settings = new HuggingFaceSettings(domain: "api.your-custom-domain.com");
            var api = new HuggingFaceClient(auth, settings);
            Console.WriteLine(api.Settings.Info.BaseRequestUrlFormat);
            Console.WriteLine(api.Settings.Info.InferenceRequestUrlFormat);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(HuggingFaceAuthentication.CONFIG_FILE))
            {
                File.Delete(HuggingFaceAuthentication.CONFIG_FILE);
            }

            HuggingFaceSettings.Default = null;
            HuggingFaceAuthentication.Default = null;
        }
    }
}
