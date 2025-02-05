using Testify.Components.Interfaces;
using Testify.Core.Implementations;
using Testify.Core.Interfaces;

namespace Testify.Components.Implementations.Abstractions
{
    public abstract class ComponentWithLabel : BaseComponent, IComponentWithLabel
    {

        protected readonly IWebComponent label;

        protected ComponentWithLabel()
        {
            var inputDescription = InitializeLabelDescription();
            label = GetComponent().WithDescription(inputDescription).Build();
        }

        protected abstract Description InitializeLabelDescription();


        public virtual string GetLabel(TimeSpan? timeout = null) => label.Properties.GetText(timeout);

        public virtual bool HasLabel(TimeSpan? timeout = null) => label.IsAvailable(timeout);
    }
    
}
