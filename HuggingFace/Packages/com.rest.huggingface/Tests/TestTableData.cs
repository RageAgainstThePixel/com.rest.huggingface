using System.Collections.Generic;

namespace Rest.HuggingFace.Tests
{
    internal class TestTableData
    {
        public TestTableData(List<string> repositories, List<string> stars, List<string> contributors, List<string> languages)
        {
            Repositories = repositories;
            Stars = stars;
            Contributors = contributors;
            Languages = languages;
        }

        public IReadOnlyList<string> Repositories { get; }

        public IReadOnlyList<string> Stars { get; }

        public IReadOnlyList<string> Contributors { get; }

        public IReadOnlyList<string> Languages { get; }
    }
}
