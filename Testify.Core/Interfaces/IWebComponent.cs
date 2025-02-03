using Testify.Core.Implementations;

namespace Testify.Core.Interfaces
{
    public interface IWebComponent : IFormattable, IEquatable<IWebComponent>, IWebComponentContext
    {
        static WebComponentConfiguration Configuration { get; set; } = null!;

        Description Description { get; }

        Properties Properties { get; }

        Refresher Refresher { get; }

        IActions Actions { get; }

        IWebComponent? Parent { get; }

        ICondition? Condition { get; }

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
