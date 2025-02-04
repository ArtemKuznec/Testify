using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using OpenQA.Selenium;
using System;

namespace Testify.Core.Implementations
{
    public class WebComponentConfiguration
    {
        public Interfaces.ISearchContext Context { get; init; }

        public IWebDriver Driver { get; }

        public TimeSpan Duration { get; init; }

        public TimeSpan Timeout { get; init; }

        public WebComponentConfiguration(IWebDriver driver)
        {
            Context = new SearchContext();
            Driver = driver.ThrowIfNull();

            Duration = TimeSpan.FromSeconds(1);
            Timeout = TimeSpan.FromSeconds(5);
        }

        public virtual Actions CreateActions(IWebComponent component) => new Actions(component);
    }
}
