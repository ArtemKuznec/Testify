using Testify.Core.Interfaces;

namespace Testify.Core.Exceptions
{
    public sealed class PropertyException : TestifyException
    {
        public PropertyException(string? message, IWebComponent component, Exception? innerException = null) : base(message, component, innerException) { }
    }
}
