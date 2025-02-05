using OpenQA.Selenium;
using TDMS.Farvater.Automatic.Tests;
using Testify.Components.Extensions;
using Testify.Components.Implementations.Primary;

namespace Testify.Tests
{
    public class ChatGPTTest : TestBase
    {
        [Fact]
        public void TestChatGPT()
        {
            Context.GetComponent<ButtonComponent>().Build().Click();

            TextAreaComponent promtTextArea = Context.GetComponent<TextAreaComponent>().ByPlaceholder("Ask ChatGPT").Build();

            promtTextArea.SetValue("What is Xunit, and what are its key features/strengths?");

            promtTextArea.SendKeys(Keys.Enter);
        }
    }
}