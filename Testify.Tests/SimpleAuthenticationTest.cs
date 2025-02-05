using System.Text.RegularExpressions;
using TDMS.Farvater.Automatic.Tests;
using Testify.Components.Extensions;
using Testify.Components.Implementations.Complex;
using Testify.Components.Implementations.Primary;
using Testify.Core.Implementations;

namespace Testify.Tests
{
    public class SimpleAuthenticationTest : TestBase
    {
        protected override Uri PageAddress => new("https://authenticationtest.com/");

        [Fact]
        public void AuthenticationTest()
        {
            CardComponent cardComponent = Context.GetComponent<CardComponent>().ByText("Interactive Authentication").Build();
            cardComponent.TryButton_Click();

            var inputDataString = Context.GetComponent().WithDescription(new(Selector.Css("div[class='alert alert-primary']"), "Input Data")).Build().Properties.GetText();
            var inputData = MatchData(inputDataString);

            InputComponent emailInput = Context.GetComponent<InputComponent>().ByLabel("E-Mail Address").Build();
            emailInput.SetValue(inputData.Email);

            InputComponent passwordInput = Context.GetComponent<InputComponent>().ByLabel("Password").Build();
            passwordInput.SetValue(inputData.Password);

            InputComponent codeInput = Context.GetComponent<InputComponent>().ByPlaceholder("123456").Build();
            codeInput.SetValue(inputData.Captcha);

            Context.GetComponent().WithDescription(new(Selector.Css("input[type='submit']"), "Submit Input")).Build().Actions.Click();
            var resultHeader = Context.GetComponent().WithDescription(new(Selector.Css("h1"), "Header")).Build();

            Assert.Equal("Login Success", resultHeader.Properties.GetText());
        }

        private (string Email, string Password, string Captcha) MatchData(string data)
        {
            string emailPattern = @"E-Mail:\s*([^\s]+)";
            string passwordPattern = @"Password:\s*([^\s]+)";
            string captchaPattern = @"Captcha code \((\d+)\)";

            Match emailMatch = Regex.Match(data, emailPattern);
            string email = emailMatch.Success ? emailMatch.Groups[1].Value : "Not found";

            Match passwordMatch = Regex.Match(data, passwordPattern);
            string password = passwordMatch.Success ? passwordMatch.Groups[1].Value : "Not found";

            Match captchaMatch = Regex.Match(data, captchaPattern);
            string captcha = captchaMatch.Success ? captchaMatch.Groups[1].Value : "Not found";

            return (email, password, captcha);
        }
    }
}
