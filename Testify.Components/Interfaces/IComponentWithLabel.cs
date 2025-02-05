namespace Testify.Components.Interfaces
{
    public interface IComponentWithLabel : IBaseComponent
    {
        bool HasLabel(TimeSpan? timeout = null);

        string GetLabel(TimeSpan? timeout = null);
    }
}
