using Testify.Core.Utilities;

namespace Testify.Core.Extensions
{
    internal static class ObjectExtensions
    {
        public static T Cast<T>(this object? @object) => @object.ThrowIfNotOfType<T>();
    }
}
