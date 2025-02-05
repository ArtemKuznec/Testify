using System;

namespace Testify.Components.Interfaces
{
    public interface IComponentWithText : IBaseComponent
    {
        bool HasText(TimeSpan? timeout = null);

        string GetText(TimeSpan? timeout = null);
    }
}
