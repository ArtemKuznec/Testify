using Testify.Core.Exceptions;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Testify.Core.Implementations
{
    public sealed class Properties
    {
        private const string ReadOnlyAttribute = "readonly";

        private const string ClassAttribute = "class";

        private const string ValueAttribute = "value";

        private const string DisplayedDescription = $"\"{nameof(IWebElement.Displayed)}\" status";

        private const string SelectedDescription = $"\"{nameof(IWebElement.Selected)}\" status";

        private const string EnabledDescription = $"\"{nameof(IWebElement.Enabled)}\" status";

        private const string LocationDescription = "location";

        private const string SizeDescription = "size";

        private const string TextDescription = "text";

        private const string TagDescription = "tag";

        private readonly IWebComponent _component;

        internal Properties(IWebComponent component) => _component = component.ThrowIfNull();

        public string? GetValue(TimeSpan? timeout = null) => GetAttribute(ValueAttribute, timeout);

        public string? GetClass(TimeSpan? timeout = null) => GetAttribute(ClassAttribute, timeout);

        public string GetText(TimeSpan? timeout = null) => Invoke(element => element.Text, TextDescription, timeout);

        public string GetTag(TimeSpan? timeout = null) => Invoke(element => element.TagName, TagDescription, timeout);

        public Point GetLocation(TimeSpan? timeout = null) => Invoke(element => element.Location, LocationDescription, timeout);

        public Size GetSize(TimeSpan? timeout = null) => Invoke(element => element.Size, SizeDescription, timeout);

        public bool IsDisplayed(TimeSpan? timeout = null) => Invoke(element => element.Displayed, DisplayedDescription, timeout);

        public bool IsSelected(TimeSpan? timeout = null) => Invoke(element => element.Selected, SelectedDescription, timeout);

        public bool IsEnabled(TimeSpan? timeout = null) => Invoke(element => element.Enabled, EnabledDescription, timeout);

        public bool IsReadOnly(TimeSpan? timeout = null)
        {
            var attributeValue = GetAttribute(ReadOnlyAttribute, timeout);
            return string.Equals(attributeValue, bool.TrueString, StringComparison.OrdinalIgnoreCase);
        }

        public string? GetAttribute(string name, TimeSpan? timeout = null) => Invoke(element => element.GetDomAttribute(name), $"\"{name}\" attribute", timeout);

        public string? GetCssProperty(string name, TimeSpan? timeout = null) => Invoke(element => element.GetCssValue(name), $"CSS \"{name}\" property", timeout);

        private TValue Invoke<TValue>(Func<IWebElement, TValue> function, string returnValueDescription, TimeSpan? timeout = null)
        {
            timeout ??= _component.Timeout;

            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                using var source = new CancellationTokenSource();

                try
                {
                    var element = _component.Refresher.Refresh(source.Token, TimeSpan.Zero);
                    return function.Invoke(element);
                }
                catch (Exception exception)
                {
                    if (stopwatch.Elapsed >= timeout.Value)
                        throw new PropertyException($"An exception occurred while retrieving the {returnValueDescription} of the web component.", _component, exception);
                }
                finally
                {
                    source.Cancel();
                }
            }
        }
    }
}
