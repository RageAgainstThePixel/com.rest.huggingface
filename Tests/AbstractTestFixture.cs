// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace HuggingFace.Tests
{
    internal abstract class AbstractTestFixture
    {
        protected readonly HuggingFaceClient HuggingFaceClient;

        public AbstractTestFixture()
        {
            var auth = new HuggingFaceAuthentication().LoadDefaultsReversed();
            var settings = new HuggingFaceSettings();
            HuggingFaceClient = new HuggingFaceClient(auth, settings);
            //HuggingFaceClient.EnableDebug = true;
        }
    }
}
