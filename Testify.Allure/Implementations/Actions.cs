using Testify.Allure.Extensions;
using Testify.Core.Implementations;
using Testify.Core.Interfaces;

namespace Testify.Allure.Implementations
{
    public sealed class AllureActions : Actions
    {
        private const string ContextClickDescription = "Context click on";

        private const string DoubleClickDescription = "Double click on";

        private const string ClearDescription = "Clear the value of";

        private const string ClickDescription = "Click on";

        private const string HoverDescription = "Hover on";

        public AllureActions(IWebComponent component) : base(component) { }

        public override Actions SendKeys(string keys, TimeSpan? timeout = null) => Invoke(timeout => base.SendKeys(keys, timeout), $"Send \"{keys}\" keys to", timeout);

        public override Actions ContextClick(TimeSpan? timeout = null) => Invoke(timeout => base.ContextClick(timeout), ContextClickDescription, timeout);

        public override Actions DoubleClick(TimeSpan? timeout = null) => Invoke(timeout => base.DoubleClick(timeout), DoubleClickDescription, timeout);

        public override Actions Hover(TimeSpan? timeout = null) => Invoke(timeout => base.Hover(timeout), HoverDescription, timeout);

        public override Actions Click(TimeSpan? timeout = null) => Invoke(timeout => base.Click(timeout), ClickDescription, timeout);

        public override Actions Clear(TimeSpan? timeout = null) => Invoke(timeout => base.Clear(timeout), ClearDescription, timeout);

        private Actions Invoke(Func<TimeSpan?, Actions> action, string actionDescription, TimeSpan? timeout = null) =>
            AllureExtensions.StartStep(() => $"{actionDescription} the \"{component}\"", () => action.Invoke(timeout));
    }
}
