namespace Testify.Core.Inerfaces
{
    public interface ICompositeRequirement<TComponent> : IRequirement<TComponent> where TComponent : IWebComponent
    {
        IReadOnlyList<IRequirement<TComponent>> Requirements { get; }
    }

}
