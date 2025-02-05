using Testify.Core.Implementations;
using Testify.Core.Interfaces;
using Testify.Components.Implementations.Abstractions;
using Testify.Components.Interfaces;

namespace Testify.Components.Implementations.Primary
{
    public sealed class InputComponent : ComponentWithValue<string>, IComponentWithLabel
    {
        public new static readonly Description DefaultDescription = new(Selector.Css("div[class='form-group']"), "Input Component");

        private static readonly Description _labelDescription = new(Selector.Css("label"), "Label");

        private static readonly Description _inputDescription = new(Selector.Css("input[class='form-control']"), "Input");

        private IWebComponent _label => GetComponent().WithDescription(_labelDescription).Build();

        protected override Description InitializeDescription() => DefaultDescription;

        protected override Description InitializeInputDescription() => _inputDescription;

        protected override string Convert(string? value) => value ?? string.Empty;

        public void ClickOnTrigger(TimeSpan? timeout = null) => _label.Actions.Click(timeout);

        public bool HasLabel(TimeSpan? timeout = null) => _label.IsAvailable(timeout);

        public string GetLabel(TimeSpan? timeout = null) => _label.Properties.GetText(timeout);
    }
}
