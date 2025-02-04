using Testify.Core.Interfaces;

namespace Testify.Core.Exceptions
{
    public sealed class RequirementException : TestifyException
    {
        public RequirementException(string? message, IWebComponent component, Exception? innerException = null) : base(message, component, innerException) { }
    }
}
