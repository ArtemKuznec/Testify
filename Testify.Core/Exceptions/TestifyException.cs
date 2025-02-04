using Testify.Core.Interfaces;

namespace Testify.Core.Exceptions
{
    public abstract class TestifyException : ArgumentException
    {
        public TestifyException(string? message, IWebComponent component, Exception? innerException = null) : base(message, component.ToString(), innerException) { }
    }
}
