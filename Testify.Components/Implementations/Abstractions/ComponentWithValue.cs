using Testify.Core.Implementations;
using Testify.Core.Interfaces;
using Testify.Components.Interfaces;

namespace Testify.Components.Implementations.Abstractions
{
    public abstract class ComponentWithValue<TValue> : BaseComponent, IComponentWithValue<TValue>
    {
        protected readonly IWebComponent input;

        protected ComponentWithValue()
        {
            var inputDescription = InitializeInputDescription();
            input = GetComponent().WithDescription(inputDescription).Build();
        }

        protected abstract TValue Convert(string? value);

        protected abstract Description InitializeInputDescription();

        protected virtual IWebComponent GetInputComponent() => GetComponent().WithDescription(InitializeInputDescription()).Build();


        public virtual TValue GetValue(TimeSpan? timeout = null)
        {
            var value = input.Properties.GetValue(timeout);
            return Convert(value);
        }

        public virtual void SetValue(TValue value, TimeSpan? timeout = null)
        {
            var keys = value?.ToString() ?? string.Empty;
            input.Actions.SendKeys(keys, timeout);
        }

        public virtual void ClickInput(TimeSpan? timeout = null)
        {
            input.Actions.Click(timeout);
        }

        public virtual void SendKeys(string keys, TimeSpan? timeout = null) => input.Actions.SendKeys(keys, timeout);

        public virtual void Clear(TimeSpan? timeout = null) => input.Actions.Clear();

        public string GetPlaceholder(TimeSpan? timeout = null) => input.Properties.GetAttribute("placeholder", timeout);
    }
}
