using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;
using Testify.Core.Inerfaces;

namespace Testify.Core.Implemetations
{
    public abstract class Selector : ISelector
    {
        public string Pattern { get; }

        private readonly By _mechanism;

        protected Selector(string pattern, Func<string, By> mechanism)
        {
            Pattern = pattern.ThrowIfNullOrWhiteSpace();
            _mechanism = mechanism.Invoke(pattern).ThrowIfNull();
        }

        public IWebElement FindElement(ISearchContext context) => _mechanism.FindElement(context);

        public IReadOnlyList<IWebElement> FindElements(ISearchContext context) => _mechanism.FindElements(context);

        public bool Equals([NotNullWhen(true)] ISelector? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Pattern == other.Pattern;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as ISelector);

        public override int GetHashCode() => Pattern.GetHashCode();

        public override string ToString() => Pattern;
    }

}
