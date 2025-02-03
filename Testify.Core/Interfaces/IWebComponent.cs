using Testify.Core.Implementations;
using System;

namespace Testify.Core.Interfaces
{
    public interface IWebComponent : IFormattable, IEquatable<IWebComponent>, IWebComponentContext
    {
        static IWebComponentConfiguration Configuration { get; set; } = null!;

        Description Description { get; }

        Refresher Refresher { get; }

        IProperties Properties { get; }

        ICondition? Condition { get; }

        IWebComponent? Parent { get; }

        IActions Actions { get; }

        TimeSpan Timeout { get; }

        int Index { get; }

        bool IsAvailable(TimeSpan? timeout = null);

        internal void SetDescription(Description description);

        internal void SetCondition(ICondition condition);

        internal void SetParent(IWebComponent parent);

        internal void SetTimeout(TimeSpan timeout);

        internal void SetIndex(int index);
    }
}
