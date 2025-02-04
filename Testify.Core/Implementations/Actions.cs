using Testify.Core.Exceptions;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace Testify.Core.Implementations
{
    public class Actions
    {
        private const string ContextClickDescription = "context clicking on";

        private const string DoubleClickDescription = "double clicking on";

        private const string ClearDescription = "clearing the value of";

        private const string ClickDescription = "clicking on";

        private const string HoverDescription = "hovering on";

        protected readonly IWebComponent component;

        public Actions(IWebComponent component) => this.component = component.ThrowIfNull();

        protected static OpenQA.Selenium.Interactions.Actions CreateActions() => new(IWebComponent.Configuration.Driver);

        public virtual Actions SendKeys(string keys, TimeSpan? timeout = null) => Invoke(element => element.SendKeys(keys), $"sending \"{keys}\" keys to", timeout);

        public virtual Actions ContextClick(TimeSpan? timeout = null) => Invoke(element => CreateActions().ContextClick(element).Perform(), ContextClickDescription, timeout);

        public virtual Actions DoubleClick(TimeSpan? timeout = null) => Invoke(element => CreateActions().DoubleClick(element).Perform(), DoubleClickDescription, timeout);

        public virtual Actions Hover(TimeSpan? timeout = null) => Invoke(element => CreateActions().MoveToElement(element).Perform(), HoverDescription, timeout);

        public virtual Actions Clear(TimeSpan? timeout = null) => Invoke(element => element.Clear(), ClearDescription, timeout);

        public virtual Actions Click(TimeSpan? timeout = null) => Invoke(element => element.Click(), ClickDescription, timeout);

        private Actions Invoke(Action<IWebElement> action, string actionDescription, TimeSpan? timeout = null)
        {
            timeout ??= component.Timeout;

            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                using var source = new CancellationTokenSource();

                try
                {
                    var element = component.Refresher.Refresh(source.Token, TimeSpan.Zero);
                    action.Invoke(element);
                    return this;
                }
                catch (Exception exception)
                {
                    if (stopwatch.Elapsed >= timeout.Value)
                        throw new ActionException($"An exception occurred while {actionDescription} the web component.", component, exception);
                }
                finally
                {
                    source.Cancel();
                }
            }
        }
    }
}
