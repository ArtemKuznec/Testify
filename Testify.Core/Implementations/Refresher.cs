using Testify.Core.Exceptions;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace Testify.Core.Implementations
{
    public sealed class Refresher
    {
        private readonly IWebComponent _component;

        private CancellationToken _token;

        private IWebElement? _element;

        internal Refresher(IWebComponent component)
        {
            _component = component.ThrowIfNull();
            _token = new CancellationToken(true);
        }

        public IWebElement Refresh(CancellationToken token, TimeSpan? timeout = null)
        {
            timeout ??= _component.Timeout;

            if (_token.IsCancellationRequested)
            {
                _token = token;
                return _element = GetElement(timeout.Value);
            }

            if (_element is null)
                return GetElement(timeout.Value);

            return _element;
        }

        private IWebElement GetElement(TimeSpan timeout)
        {
            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                try
                {
                    var parent = _component.Parent?.Refresher.Refresh(_token, TimeSpan.Zero);

                    var elements = parent is not null
                        ? _component.Description.Selector.FindElements(parent)
                        : _component.Description.Selector.FindElements(IWebComponent.Configuration.Driver);

                    if (_component.Description.Index >= elements.Count)
                        throw new RefresherException($"The description index ({_component.Description.Index}) of the web component must be less than the available web elements ({elements.Count}).", _component);

                    if (_component.Condition is null)
                    {
                        _component.SetIndex(_component.Description.Index);
                        return elements[_component.Description.Index];
                    }

                    if (_component.Condition.Enabled == false)
                        if (_component.Index >= elements.Count)
                            throw new RefresherException($"The index ({_component.Index}) of the web component must be less than the available web elements ({elements.Count}).", _component);
                        else return elements[_component.Index];

                    var match = 0;

                    for (var index = 0; index < elements.Count; index++)
                    {
                        _component.SetIndex(index);
                        _element = elements[index];

                        if (_component.Condition.Execute(TimeSpan.Zero))
                            if (_component.Description.Index == match++)
                                return elements[index];
                    }

                    throw new RefresherException($"Found {match} web elements that match the condition, but {_component.Description.Index + 1} were expected.", _component);
                }
                catch (Exception exception)
                {
                    if (stopwatch.Elapsed >= timeout)
                        throw new RefresherException($"An exception occurred while retrieving a web element of the web component.", _component, exception);
                }
            }
        }
    }
}
