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
        private readonly IWebComponent _component;

        private static Interactions.Actions CreateActions() => new(IWebComponent.Configuration.Driver);

        internal Actions(IWebComponent component) => _component = component.ThrowIfNull();

        public virtual IActions SendKeys(string keys, TimeSpan? timeout = null)
        {
            var exceptionMessage = $"Не удалось отправить клавиши '{keys}' для"; // TODO 
            return Invoke(element => { element.SendKeys(keys.ToString()); }, exceptionMessage, timeout);
        }

        public virtual IActions ContextClick(TimeSpan? timeout = null)
        {
            var exceptionMessage = $"Не удалось выполнить контекстное нажатие"; // TODO 
            return Invoke(element => { CreateActions().ContextClick(element).Perform(); }, exceptionMessage, timeout);
        }

        public virtual IActions DoubleClick(TimeSpan? timeout = null)
        {
            var exceptionMessage = $"Не удалось дважды нажать на"; // TODO 
            return Invoke(element => { CreateActions().DoubleClick(element).Perform(); }, exceptionMessage, timeout);
        }

        public virtual IActions Hover(TimeSpan? timeout = null)
        {
            var exceptionMessage = $"Не удалось навести на"; // TODO 
            return Invoke(element => { CreateActions().MoveToElement(element).Perform(); }, exceptionMessage, timeout);
        }

        public virtual IActions Click(TimeSpan? timeout = null)
        {
            var exceptionMessage = $"Не удалось нажать на"; // TODO 
            return Invoke(element => { element.Click(); }, exceptionMessage, timeout);
        }

        public virtual IActions Clear(TimeSpan? timeout = null)
        {
            var exceptionMessage = $"Не удалось очистить"; // TODO 
            return Invoke(element => { element.Clear(); }, exceptionMessage, timeout);
        }

        private IActions Invoke(Action<IWebElement> action, string exceptionMessage, TimeSpan? timeout = null)
        {
            timeout ??= _component.Timeout;

            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                using var source = new CancellationTokenSource();

                try
                {
                    action.Invoke(_component.Refresher.Refresh(source.Token, TimeSpan.Zero));
                    return this;
                }
                catch (Exception exception)
                {
                    if (stopwatch.Elapsed >= timeout.Value)
                        throw new Exception(exceptionMessage, exception); // TODO
                }
                finally
                {
                    source.Cancel();
                }
            }
        }
    }
}
