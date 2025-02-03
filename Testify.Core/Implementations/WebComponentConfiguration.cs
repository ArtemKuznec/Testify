using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using OpenQA.Selenium;

namespace Testify.Core.Implementations
{
    public class WebComponentConfiguration
    {
        public IWebComponentContext Context { get; protected set; }

        public IWebDriver Driver { get; protected set; }

        public TimeSpan Duration { get; protected set; }

        public TimeSpan Timeout { get; protected set; }

        public WebComponentConfiguration(IWebDriver driver)
        {
            Context = new WebComponentContext();
            Driver = driver.ThrowIfNull();

            Duration = TimeSpan.FromSeconds(1);
            Timeout = TimeSpan.FromSeconds(5);
        }

        public virtual Actions CreateActions(IWebComponent component) => new(component);
    }
}
