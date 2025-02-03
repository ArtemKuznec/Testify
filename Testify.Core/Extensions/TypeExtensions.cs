using System;
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
            {
                throw new ArgumentException(string.Empty, nameof(type)); // TODO: Изменить сообщение.
            }

            if (instanceType.IsAssignableTo(genericType) == false)
            {
                throw new ArgumentException(string.Empty, nameof(type)); // TODO: Изменить сообщение.
            }

            var instance = Activator.CreateInstance(instanceType, Flags, null, args, null);

            return instance.Cast<T>();
        }
    }
}

