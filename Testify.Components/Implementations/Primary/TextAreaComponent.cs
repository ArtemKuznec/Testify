using Testify.Core.Implementations;
using Testify.Components.Implementations.Abstractions;
using Testify.Core.Interfaces;

namespace Testify.Components.Implementations.Primary
{
    public sealed class TextAreaComponent : ComponentWithValue<string>
    {
        public new static readonly Description DefaultDescription = new(Selector.Css("div[class^='input-box']"), "Text Area Component");

        private static readonly Description _inputDescription = new(Selector.Css("textarea[class^='textarea']"), "Input Component");

        protected override Description InitializeDescription() => DefaultDescription;

        protected override Description InitializeInputDescription() => _inputDescription;

        protected override string Convert(string? value) => value ?? string.Empty;
    }
}
