using Testify.Core.Interfaces;
using System;

namespace Testify.Core.Exceptions
{
    public sealed class ActionException : TestifyException
    {
        public ActionException(string? message, IWebComponent component, Exception? innerException = null) : base(message, component, innerException) { }
    }
}
