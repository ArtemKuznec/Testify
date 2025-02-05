using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Testify.Automatic
{
    internal static class Browsers
    {
        public static class Chrome
        {
            public static IWebDriver Local() => new ChromeDriver();
        }
    }
}
