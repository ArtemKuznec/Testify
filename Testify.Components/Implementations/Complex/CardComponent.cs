using Testify.Components.Implementations.Abstractions;
using Testify.Core.Implementations;
using Testify.Core.Interfaces;

namespace Testify.Components.Implementations.Complex
{
    public class CardComponent : ComponentWithText
    {
        public new static readonly Description DefaultDescription = new(Selector.Css("div[class^='card text-center']"), "Card Component");

        private static readonly Description _textDescription = new(Selector.Css("*[class^='card-title']"), "Card Title Text");

        private IWebComponent _tryButton => GetComponent().WithDescription(new(Selector.Css("a[class^='btn']"), "Try Button")).Build();

        protected override Description InitializeDescription() => DefaultDescription;

        protected override Description InitializeTextDescription() => _textDescription;

        public void TryButton_Click() => _tryButton.Actions.Click();
    }
}
