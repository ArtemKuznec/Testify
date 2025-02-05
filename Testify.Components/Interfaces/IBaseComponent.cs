using Testify.Core.Interfaces;
using System;

namespace Testify.Components.Interfaces
{
    public interface IBaseComponent : IWebComponent
    {
        string GetSystemIdentifier(TimeSpan? timeout = null);
    }
}
