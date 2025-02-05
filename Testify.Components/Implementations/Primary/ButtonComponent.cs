using Testify.Core.Implementations;
using Testify.Components.Implementations.Abstractions;

namespace Testify.Components.Implementations.Primary
{
    public sealed class ButtonComponent : ComponentWithText
    {
        public new static readonly Description DefaultDescription = new(Selector.Css("button[aria-label^='New chat']"), "Button");

        private static readonly Description _textDescription = new(Selector.Css("div[class^='button-text']"), "Button Text");

        protected override Description InitializeDescription() => DefaultDescription;

        protected override Description InitializeTextDescription() => _textDescription;

        public void Click(TimeSpan? timeout = null) => Actions.Click(timeout);
    }
}
