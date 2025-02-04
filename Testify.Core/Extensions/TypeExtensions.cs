using System.Reflection;

namespace Testify.Core.Extensions
{
    internal static class TypeExtensions
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public static T CreateInstance<T>(this Type? type, params object[] args)
        {
            var genericType = typeof(T);
            var instanceType = type ?? genericType;

            if (instanceType.IsInterface)
                throw new Exception("A type cannot be an interface.");

            if (instanceType.IsAssignableTo(genericType) == false)
                throw new ArgumentException($"The type \"{instanceType.FullName}\" must be assignable to {genericType.FullName}.", nameof(type));

            var instance = Activator.CreateInstance(instanceType, Flags, null, args, null);

            return instance.Cast<T>();
        }
    }
}

