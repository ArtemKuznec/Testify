using System.Diagnostics.CodeAnalysis;
using Testify.Core.Inerfaces;
using Testify.Core.Utilities;

namespace Testify.Core.Implemetations
{
    public sealed class Description : IDescription
    {
        private const int MinIndex = 0;

        public ISelector Selector { get; }

        public string Name { get; }

        public int Index { get; }

        public Description(ISelector selector, string name, int index = MinIndex)
        {
            Selector = selector.ThrowIfNull();
            Index = index.ThrowIfLessThan(MinIndex);
            Name = name.ThrowIfNullOrWhiteSpace();
        }

        public bool Equals([NotNullWhen(true)] IDescription? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Selector.Equals(other.Selector);

            result &= Index == other.Index;
            result &= Name == other.Name;

            return result;
        }

        public IDescription With(ISelector selector) => new Description(selector, Name, Index);

        public IDescription With(string name) => new Description(Selector, name, Index);

        public IDescription With(int index) => new Description(Selector, Name, index);

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as IDescription);

        public override int GetHashCode() => HashCode.Combine(Selector, Name, Index);

        public override string ToString() => ToString(StringFormat.General);

        public string ToString(string? format, IFormatProvider? _ = null)
        {
            if (string.IsNullOrWhiteSpace(format))
                format = StringFormat.General;

            return format.ToUpperInvariant() switch
            {
                StringFormat.General => $"{nameof(Name)} = \"{Name}\", {nameof(Selector)} = \"{Selector.Pattern}\", {nameof(Index)} = {Index}",
                StringFormat.Short => Index == MinIndex ? $"{Name}" : $"{Name} ({Index})",
                _ => throw new Exception(),
            };
        }
    }

}
