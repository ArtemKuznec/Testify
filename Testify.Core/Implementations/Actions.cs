using Testify.Core.Exceptions;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;
using Interactions = OpenQA.Selenium.Interactions;

namespace Testify.Core.Implementations
{
    public class Actions : IActions
    {
        private const string ContextClickDescription = "context clicking on";

        private const string DoubleClickDescription = "double clicking on";

        private const string ClearDescription = "clearing the value of";

        private const string ClickDescription = "clicking on";

        private const string HoverDescription = "hovering on";

        private readonly IWebComponent _component;

        public Actions(IWebComponent component) => _component = component.ThrowIfNull();

        private static Interactions.Actions CreateActions() => new(IWebComponent.Configuration.Driver);

        public virtual IActions SendKeys(string keys, TimeSpan? timeout = null) => Invoke(element => element.SendKeys(keys), $"sending \"{keys}\" keys to", timeout);

        public virtual IActions ContextClick(TimeSpan? timeout = null) => Invoke(element => CreateActions().ContextClick(element).Perform(), ContextClickDescription, timeout);

        public virtual IActions DoubleClick(TimeSpan? timeout = null) => Invoke(element => CreateActions().DoubleClick(element).Perform(), DoubleClickDescription, timeout);

        public virtual IActions Hover(TimeSpan? timeout = null) => Invoke(element => CreateActions().MoveToElement(element).Perform(), HoverDescription, timeout);

        public virtual IActions Clear(TimeSpan? timeout = null) => Invoke(element => element.Clear(), ClearDescription, timeout);

        public virtual IActions Click(TimeSpan? timeout = null) => Invoke(element => element.Click(), ClickDescription, timeout);

        private Actions Invoke(Action<IWebElement> action, string actionErrorDescription, TimeSpan? timeout = null)
        {
            timeout ??= _component.Timeout;

            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                using var source = new CancellationTokenSource();

                try
                {
                    var element = _component.Refresher.Refresh(source.Token, TimeSpan.Zero);
                    action.Invoke(element);
                    return this;
                }
                catch (Exception exception)
                {
                    if (stopwatch.Elapsed >= timeout.Value)
                        throw new ActionException($"An exception occurred while {actionErrorDescription} the web component.", _component, exception);
                }
                finally
                {
                    source.Cancel();
                }
            }
        }
    }
}
