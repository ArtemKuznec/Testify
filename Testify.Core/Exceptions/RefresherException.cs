using Testify.Core.Interfaces;
using System;

namespace Testify.Core.Exceptions
{
    public sealed class RefresherException : TestifyException
    {
        public RefresherException(string? message, IWebComponent component, Exception? innerException = null) : base(message, component, innerException) { }
    }
}
