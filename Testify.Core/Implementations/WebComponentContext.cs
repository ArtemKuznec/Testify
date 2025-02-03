using Testify.Core.Interfaces;

namespace Testify.Core.Implementations
{
    public class WebComponentContext : IWebComponentContext
    {
        private readonly IWebComponent? _parent;

        public WebComponentContext(IWebComponent? parent = null) => _parent = parent;

        public WebComponentCollectionBuilder<TComponent> GetComponents<TComponent>() where TComponent : IWebComponent => new(_parent);

        public WebComponentBuilder<TComponent> GetComponent<TComponent>() where TComponent : IWebComponent => new(_parent);

        public virtual WebComponentCollectionBuilder<IWebComponent> GetComponents() => new WebComponentCollectionBuilder<IWebComponent>(_parent).WithType<WebComponent>();

        public virtual WebComponentBuilder<IWebComponent> GetComponent() => new WebComponentBuilder<IWebComponent>(_parent).WithType<WebComponent>();
    }
}
