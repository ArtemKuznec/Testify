using OpenQA.Selenium;
using Testify.Allure.Implementations;
using Testify.Core.Implementations;
using Testify.Core.Interfaces;


namespace Testify.Automatic
{
    internal sealed class Configuration(IWebDriver driver) : WebComponentConfiguration(driver)
    {
        public override Actions CreateActions(IWebComponent component) => new AllureActions(component);
    }
}
