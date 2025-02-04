using Testify.Allure.Extensions;
using Testify.Core.Implementations;
using Testify.Core.Interfaces;
using System;
using System.Drawing;
using Interactions = OpenQA.Selenium.Interactions;

namespace Testify.Allure.Implementations
{
    public sealed class AllureActionBuilder : ActionBuilder
    {
        public AllureActionBuilder() { }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private AllureActionBuilder(ActionBuilder builder, Action<Interactions.Actions> action) : base(builder, action) { }

        private static void Invoke(Action action, IWebComponent component, string actionDescription) => AllureExtensions.StartStep($"{actionDescription} the \"{component}\"", action);

        private static void Invoke(Action action, string actionDescription) => AllureExtensions.StartStep(actionDescription, action);

        public override void Perform() =>
            Invoke(base.Perform, "Sequence of actions");

        protected override void ClickAndHold(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.ClickAndHold(builder, component, timeout), component, "Click and hold on");

        protected override void ClickAndHold(Interactions.Actions builder) =>
            Invoke(() => base.ClickAndHold(builder), "Click and hold");

        protected override void ContextClick(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.ContextClick(builder, component, timeout), component, "Context click on");

        protected override void ContextClick(Interactions.Actions builder) =>
            Invoke(() => base.ContextClick(builder), "Context click");

        protected override void DoubleClick(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.DoubleClick(builder, component, timeout), component, "Double click on");

        protected override void DoubleClick(Interactions.Actions builder) =>
            Invoke(() => base.DoubleClick(builder), "Double click");

        protected override void Click(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.Click(builder, component, timeout), component, "Click on");

        protected override void Click(Interactions.Actions builder) =>
            Invoke(() => base.Click(builder), "Click");

        protected override void DragAndDrop(Interactions.Actions builder, IWebComponent source, IWebComponent target, TimeSpan? timeout = null) =>
            Invoke(() => base.DragAndDrop(builder, source, target, timeout), $"Darg the \"{source}\" and drop to the \"{target}\"");

        protected override void DragAndDrop(Interactions.Actions builder, IWebComponent component, Point offset, TimeSpan? timeout = null) =>
            Invoke(() => base.DragAndDrop(builder, component, offset, timeout), $"Drag and drop the \"{component}\" with offset {offset}");

        protected override void KeyDown(Interactions.Actions builder, IWebComponent component, string key, TimeSpan? timeout = null) =>
            Invoke(() => base.KeyDown(builder, component, key, timeout), component, $"Key down \"{key}\" on");

        protected override void KeyDown(Interactions.Actions builder, string key) =>
            Invoke(() => base.KeyDown(builder, key), $"Key down \"{key}\"");

        protected override void KeyUp(Interactions.Actions builder, IWebComponent component, string key, TimeSpan? timeout = null) =>
            Invoke(() => base.KeyUp(builder, component, key, timeout), component, $"Key up \"{key}\" on");

        protected override void KeyUp(Interactions.Actions builder, string key) =>
            Invoke(() => base.KeyUp(builder, key), $"Key up \"{key}\"");

        protected override void MoveToComponent(Interactions.Actions builder, IWebComponent component, Point offset, TimeSpan? timeout = null) =>
            Invoke(() => base.MoveToComponent(builder, component, offset, timeout), $"Move to the \"{component}\" with offset {offset}");

        protected override void MoveToComponent(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.MoveToComponent(builder, component, timeout), component, "Move to");

        protected override void Release(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.Release(builder, component, timeout), component, "Release on");

        protected override void Release(Interactions.Actions builder) =>
            Invoke(() => base.Release(builder), "Release");

        protected override void SendKeys(Interactions.Actions builder, IWebComponent component, string keys, TimeSpan? timeout = null) =>
            Invoke(() => base.SendKeys(builder, component, keys, timeout), $"Send keys \"{keys}\" to");

        protected override void SendKeys(Interactions.Actions builder, string keys) =>
            Invoke(() => base.SendKeys(builder, keys), $"Send keys \"{keys}\"");

        protected override void ScrollToComponent(Interactions.Actions builder, IWebComponent component, TimeSpan? timeout = null) =>
            Invoke(() => base.ScrollToComponent(builder, component, timeout), component, $"Scroll to");

        protected override void MoveByOffset(Interactions.Actions builder, Point offset) =>
            Invoke(() => base.MoveByOffset(builder, offset), $"Move by offset {offset}");

        protected override void Pause(Interactions.Actions builder, TimeSpan duration) =>
            Invoke(() => base.Pause(builder, duration), $"Pause {duration.Milliseconds} milliseconds");
    }
}
