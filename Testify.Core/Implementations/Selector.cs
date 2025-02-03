using Testify.Core.Utilities;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace Testify.Core.Implementations
{
    public sealed class Selector
    {
        public string Pattern { get; }

        private readonly By _mechanism;

        private Selector(string pattern, Func<string, By> mechanism)
        {
            Pattern = pattern.ThrowIfNullOrWhiteSpace();
            _mechanism = mechanism.Invoke(pattern).ThrowIfNull();
        }

        public static bool operator ==(Selector left, Selector right) => left.Equals(right);

        public static bool operator !=(Selector left, Selector right) => left.Equals(right) == false;

        public static Selector Css(string pattern) => new(pattern, By.CssSelector);

        public static Selector XPath(string pattern) => new(pattern, By.XPath);

        public IWebElement FindElement(ISearchContext context) => _mechanism.FindElement(context);

        public IReadOnlyList<IWebElement> FindElements(ISearchContext context) => _mechanism.FindElements(context);

        public bool Equals([NotNullWhen(true)] Selector? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Pattern == other.Pattern;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as Selector);

        public override int GetHashCode() => Pattern.GetHashCode();

        public override string ToString() => Pattern;
    }
}
