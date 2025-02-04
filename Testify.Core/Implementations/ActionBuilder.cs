using Testify.Core.Extensions;
using Testify.Core.Interfaces;
using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Threading;
using Interactions = OpenQA.Selenium.Interactions;

namespace Testify.Core.Implementations
{
    public class ActionBuilder
    {
        private readonly Action<Interactions.Actions>? _action;

        public ActionBuilder() { }

        protected ActionBuilder(ActionBuilder builder, Action<Interactions.Actions> action)
        {
            if (builder._action is not null)
            {
                _action = new Action<Interactions.Actions>(builder._action);
                _action += action;
            }
            else _action = action;
        }

        private ActionBuilder CreateInstance(ActionBuilder builder, Action<Interactions.Actions> action) => TypeExtensions.CreateInstance<ActionBuilder>(GetType(), builder, action);

        protected static void Action(Interactions.Actions builder, Func<Interactions.Actions, IWebElement, IWebElement, Interactions.IAction> action, IWebComponent left, IWebComponent right, string actionDescription, TimeSpan? timeout = null)
        {
            using var source = new CancellationTokenSource();

            try
            {
                var leftElement = left.Refresher.Refresh(source.Token, timeout);
                var rightElement = ReferenceEquals(left, right) ? leftElement : right.Refresher.Refresh(source.Token, timeout);

                builder.Reset();
                action.Invoke(builder, leftElement, rightElement).Perform();
            }
            catch (Exception exception)
            {
                throw new Exception(actionDescription, exception);
            }
            finally
            {
                source.Cancel();
            }
        }

        protected static void Action(Interactions.Actions builder, Func<Interactions.Actions, IWebElement, Interactions.IAction> action, IWebComponent component, string actionDescription, TimeSpan? timeout = null)
        {
            using var source = new CancellationTokenSource();

            try
            {
                var element = component.Refresher.Refresh(source.Token, timeout);

                builder.Reset();
                action.Invoke(builder, element).Perform();
            }
            catch (Exception exception)
            {
                throw new Exception(actionDescription, exception);
            }
            finally
            {
                source.Cancel();
            }
        }

        protected static void Action(Interactions.Actions builder, Func<Interactions.Actions, Interactions.IAction> action, string actionDescription)
        {
            try
            {
                builder.Reset();
                action.Invoke(builder).Perform();
            }
            catch (Exception exception)
            {
                throw new Exception(actionDescription, exception);
            }
        }

        public ActionBuilder MoveToComponent(IWebComponent component, Point offset, TimeSpan? timeout = null) => CreateInstance(this, builder => MoveToComponent(builder, component, offset, timeout));

        public ActionBuilder MoveToComponent(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => MoveToComponent(builder, component, timeout));

        public ActionBuilder DragAndDrop(IWebComponent source, IWebComponent target, TimeSpan? timeout = null) => CreateInstance(this, builder => DragAndDrop(builder, source, target, timeout));

        public ActionBuilder DragAndDrop(IWebComponent component, Point offset, TimeSpan? timeout = null) => CreateInstance(this, builder => DragAndDrop(builder, component, offset, timeout));

        public ActionBuilder SendKeys(string keys, IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => SendKeys(builder, component, keys, timeout));

        public ActionBuilder SendKeys(string keys) => CreateInstance(this, builder => SendKeys(builder, keys));

        public ActionBuilder KeyDown(string key, IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => KeyDown(builder, component, key, timeout));

        public ActionBuilder KeyDown(string key) => CreateInstance(this, builder => KeyDown(builder, key));

        public ActionBuilder KeyUp(string key, IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => KeyUp(builder, component, key, timeout));

        public ActionBuilder KeyUp(string key) => CreateInstance(this, builder => KeyUp(builder, key));

        public ActionBuilder ClickAndHold(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => ClickAndHold(builder, component, timeout));

        public ActionBuilder ClickAndHold() => CreateInstance(this, ClickAndHold);

        public ActionBuilder ContextClick(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => ContextClick(builder, component, timeout));

        public ActionBuilder ContextClick() => CreateInstance(this, ContextClick);

        public ActionBuilder DoubleClick(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => DoubleClick(builder, component, timeout));

        public ActionBuilder DoubleClick() => CreateInstance(this, DoubleClick);

        public ActionBuilder Release(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => Release(builder, component, timeout));

        public ActionBuilder Release() => CreateInstance(this, Release);

        public ActionBuilder Click(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => Click(builder, component, timeout));

        public ActionBuilder Click() => CreateInstance(this, Click);

        public ActionBuilder ScrollToComponent(IWebComponent component, TimeSpan? timeout = null) => CreateInstance(this, builder => ScrollToComponent(builder, component, timeout));

        public ActionBuilder MoveByOffset(Point offset) => CreateInstance(this, builder => MoveByOffset(builder, offset));

        public ActionBuilder Pause(TimeSpan duration) => CreateInstance(this, builder => Pause(builder, duration));

        public virtual void Perform() => _action?.Invoke(new Interactions.Actions(IWebComponent.Configuration.Driver));

        protected virtual void DragAndDrop(Interactions.Actions builder, IWebComponent component, Point offset, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.DragAndDropToOffset(element, offset.X, offset.Y), component, string.Empty, timeout);

        protected virtual void DragAndDrop(Interactions.Actions builder, IWebComponent source, IWebComponent target, TimeSpan? timeout = null) => Action(builder, (builder, source, target) => builder.DragAndDrop(source, target), source, target, string.Empty, timeout);

        protected virtual void MoveToComponent(Interactions.Actions builder, IWebComponent component, Point offset, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.MoveToElement(element, offset.X, offset.Y), component, string.Empty, timeout);

        protected virtual void MoveToComponent(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.MoveToElement(element), component, string.Empty, timeout);

        protected virtual void SendKeys(Interactions.Actions builder, IWebComponent component, string keys, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.SendKeys(element, keys), component, string.Empty, timeout);

        protected virtual void SendKeys(Interactions.Actions builder, string keys) => Action(builder, builder => builder.SendKeys(keys), string.Empty);

        protected virtual void KeyDown(Interactions.Actions builder, IWebComponent component, string key, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.KeyDown(element, key), component, $"Key down \"{key}\" to", timeout);

        protected virtual void KeyDown(Interactions.Actions builder, string key) => Action(builder, builder => builder.KeyUp(key), $"Key down \"{key}\"");

        protected virtual void KeyUp(Interactions.Actions builder, IWebComponent component, string key, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.KeyDown(element, key), component, string.Empty, timeout);

        protected virtual void KeyUp(Interactions.Actions builder, string key) => Action(builder, builder => builder.KeyUp(key), string.Empty);

        protected virtual void ClickAndHold(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.ClickAndHold(element), component, string.Empty, timeout);

        protected virtual void ClickAndHold(Interactions.Actions builder) => Action(builder, builder => builder.ClickAndHold(), string.Empty);

        protected virtual void ContextClick(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.ContextClick(element), component, string.Empty, timeout);

        protected virtual void ContextClick(Interactions.Actions builder) => Action(builder, builder => builder.ContextClick(), string.Empty);

        protected virtual void DoubleClick(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.DoubleClick(element), component, string.Empty, timeout);

        protected virtual void DoubleClick(Interactions.Actions builder) => Action(builder, builder => builder.DoubleClick(), string.Empty);

        protected virtual void Click(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.Click(element), component, "Click on", timeout);

        protected virtual void Click(Interactions.Actions builder) => Action(builder, builder => builder.Click(), string.Empty);

        protected virtual void Release(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.Release(element), component, string.Empty, timeout);

        protected virtual void Release(Interactions.Actions builder) => Action(builder, builder => builder.Release(), string.Empty);

        protected virtual void ScrollToComponent(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) => Action(builder, (builder, element) => builder.ScrollToElement(element), component, string.Empty, timeout);

        protected virtual void MoveByOffset(Interactions.Actions builder, Point offset) => Action(builder, builder => builder.MoveByOffset(offset.X, offset.Y), string.Empty);

        protected virtual void Pause(Interactions.Actions builder, TimeSpan duration) => Action(builder, builder => builder.Pause(duration), string.Empty);
    }
}
