using Testify.Allure.Extensions;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;

namespace Testify.Allure.Implementations
{
    public sealed class AllureActions : IActions
    {
        private const string ContextClickDescription = "Context click on";

        private const string DoubleClickDescription = "Double click on";

        private const string ClearDescription = "Clear the value of";

        private const string ClickDescription = "Click on";

        private const string HoverDescription = "Hover on";

        private readonly IActions _action;

        private readonly IWebComponent _component;

        public AllureActions(IActions actions, IWebComponent component)
        {
            _action = actions.ThrowIfNull();
            _component = component.ThrowIfNull();
        }

        public IActions SendKeys(string keys, TimeSpan? timeout = null) => Invoke(timeout => _action.SendKeys(keys, timeout), $"Send \"{keys}\" keys to", timeout);

        public IActions ContextClick(TimeSpan? timeout = null) => Invoke(timeout => _action.ContextClick(timeout), ContextClickDescription, timeout);

        public IActions DoubleClick(TimeSpan? timeout = null) => Invoke(timeout => _action.DoubleClick(timeout), DoubleClickDescription, timeout);

        public IActions Hover(TimeSpan? timeout = null) => Invoke(timeout => _action.Hover(timeout), HoverDescription, timeout);

        public IActions Click(TimeSpan? timeout = null) => Invoke(timeout => _action.Click(timeout), ClickDescription, timeout);

        public IActions Clear(TimeSpan? timeout = null) => Invoke(timeout => _action.Clear(timeout), ClearDescription, timeout);

        private IActions Invoke(Func<TimeSpan?, IActions> action, string actionDescription, TimeSpan? timeout = null) =>
            AllureExtensions.StartStep(() => $"{actionDescription} the \"{_component}\"", () => action.Invoke(timeout));
    }
}
