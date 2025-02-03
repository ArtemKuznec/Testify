namespace Testify.Core.Inerfaces
{
    public interface IDescription : IEquatable<IDescription>, IFormattable
    {
        ISelector Selector { get; }

        string Name { get; }

        int Index { get; }

        IDescription With(ISelector selector);

        IDescription With(string name);

        IDescription With(int index);
    }

}
