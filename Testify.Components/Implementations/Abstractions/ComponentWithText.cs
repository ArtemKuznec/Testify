using Testify.Core.Implementations;
using Testify.Core.Interfaces;
using Testify.Components.Interfaces;
using System;

namespace Testify.Components.Implementations.Abstractions
{
    public abstract class ComponentWithText : BaseComponent, IComponentWithText
    {
        protected readonly IWebComponent text;

        protected ComponentWithText()
        {
            var inputDescription = InitializeTextDescription();
            text = GetComponent().WithDescription(inputDescription).Build();
        }

        protected abstract Description InitializeTextDescription();

        public virtual string GetText(TimeSpan? timeout = null) => text.Properties.GetText(timeout);

        public virtual bool HasText(TimeSpan? timeout = null) => text.IsAvailable(timeout);

        protected virtual IWebComponent GetTextComponent() => GetComponent().WithDescription(InitializeTextDescription()).Build();
    }
}
