using System;

namespace Testify.Core.Interfaces
{
    public interface IActions
    {
        IActions SendKeys(string keys, TimeSpan? timeout = null);

        IActions ContextClick(TimeSpan? timeout = null);

        IActions DoubleClick(TimeSpan? timeout = null);

        IActions Click(TimeSpan? timeout = null);

        IActions Hover(TimeSpan? timeout = null);

        IActions Clear(TimeSpan? timeout = null);
    }
}
