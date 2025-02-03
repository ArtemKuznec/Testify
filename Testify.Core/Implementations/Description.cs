using Testify.Core.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Testify.Core.Implementations
{
    public sealed class Description
    {
        private const int Zero = 0;

        public Selector Selector { get; }

        public string Name { get; }

        public int Index { get; }

        public Description(Selector selector, string name, int index = Zero)
        {
            Selector = selector.ThrowIfNull();
            Index = index.ThrowIfLessThan(Zero);
            Name = name.ThrowIfNullOrWhiteSpace();
        }

        public static bool operator ==(Description left, Description right) => left.Equals(right);

        public static bool operator !=(Description left, Description right) => left.Equals(right) == false;

        public Description With(Selector selector) => new(selector, Name, Index);

        public Description With(string name) => new(Selector, name, Index);

        public Description With(int index) => new(Selector, Name, index);

        public bool Equals([NotNullWhen(true)] Description? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Selector == other.Selector;

            result &= Index == other.Index;
            result &= Name == other.Name;

            return result;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as Description);

        public override int GetHashCode() => HashCode.Combine(Selector, Name, Index);

        public override string ToString() => $"{nameof(Name)} = \"{Name}\", {nameof(Selector)} = \"{Selector}\", {nameof(Index)} = {Index}";
    }
}
