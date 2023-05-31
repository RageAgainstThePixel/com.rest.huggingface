// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace HuggingFace.Hub
{
    public sealed class OneOrMoreOf<T>
    {
        public OneOrMoreOf(T value)
            => Values = new List<T> { value };

        public OneOrMoreOf(IEnumerable<T> values)
            => Values = values.ToList();

        public IReadOnlyList<T> Values { get; }

        public static implicit operator OneOrMoreOf<T>(T value) => new OneOrMoreOf<T>(value);
    }
}
