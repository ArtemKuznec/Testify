using Testify.Core.Implementations;
using System;

namespace Testify.Core.Interfaces
{
    public interface IWebComponent : IEquatable<IWebComponent>, ISearchContext
    {
        static WebComponentConfiguration Configuration { get; set; } = null!;

        Description Description { get; }

        Properties Properties { get; }

        Refresher Refresher { get; }

        Actions Actions { get; }

        IWebComponent? Parent { get; }

        ICondition? Condition { get; }

        TimeSpan Timeout { get; }

        int Index { get; }

        bool IsAvailable(TimeSpan? timeout = null);

        internal void SetCondition(ICondition condition);

        internal void SetDescription(Description description);

        internal void SetIndex(int index);

        internal void SetParent(IWebComponent parent);

        internal void SetTimeout(TimeSpan timeout);
    }
}
