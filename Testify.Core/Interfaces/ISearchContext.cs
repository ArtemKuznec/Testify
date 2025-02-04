using Testify.Core.Implementations;

namespace Testify.Core.Interfaces
{
    public interface ISearchContext
    {
        WebComponentCollectionBuilder<TComponent> GetComponents<TComponent>() where TComponent : IWebComponent;

        WebComponentBuilder<TComponent> GetComponent<TComponent>() where TComponent : IWebComponent;

        WebComponentCollectionBuilder<IWebComponent> GetComponents();

        WebComponentBuilder<IWebComponent> GetComponent();
    }
}
