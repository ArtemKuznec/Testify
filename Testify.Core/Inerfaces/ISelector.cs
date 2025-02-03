using OpenQA.Selenium;


namespace Testify.Core.Inerfaces
{
    public interface ISelector : IEquatable<ISelector>
    {
        string Pattern { get; }

        IReadOnlyList<IWebElement> FindElements(ISearchContext context);

        IWebElement FindElement(ISearchContext context);
    }

}
