

namespace Testify.Core.Inerfaces
{
    public interface IWebComponent : IFormattable, IEquatable<IWebComponent>
    {
        IDescription Description { get; }
    }

}
