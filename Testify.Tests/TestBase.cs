using OpenQA.Selenium;
using Testify.Automatic;
using Testify.Core.Interfaces;

namespace TDMS.Farvater.Automatic.Tests
{
    public abstract class TestBase
    {
        protected static Testify.Core.Interfaces.ISearchContext Context => IWebComponent.Configuration.Context;

        protected static IWebDriver Driver => IWebComponent.Configuration.Driver;

        protected virtual TimeSpan Timeout => TimeSpan.FromSeconds(5);

        protected virtual Uri PageAddress => new("https://chatgptchatapp.com/");

        protected TestBase() => SetConfiguration();

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Driver.Dispose();
                IWebComponent.Configuration = null!;
            }
        }

        private void SetConfiguration()
        {
            var driver = Browsers.Chrome.Local();

            IWebComponent.Configuration = new Configuration(driver)
            {
                Duration = TimeSpan.FromSeconds(1),
                Context = new Testify.Core.Implementations.SearchContext(),
                Timeout = Timeout,
            };

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(PageAddress);
        }

        public void TearDown()
        {
            Driver.Quit();
        }

    }
}
