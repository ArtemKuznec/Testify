using OpenQA.Selenium;
using System;

namespace Testify.Core.Interfaces
{
    public interface IWebComponentConfiguration
    {
        IWebComponentContext Context { get; }

        IWebDriver Driver { get; }

        TimeSpan Timeout { get; }
    }
}
