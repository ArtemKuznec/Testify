using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Testify.Core.Utilities
{
    public static class Guard
    {
        private const string Empty = "";

        public static string ThrowIfNullOrWhiteSpace([NotNull] this string? parameterValue, [CallerArgumentExpression(nameof(parameterValue))] string parameterName = Empty)
        {
            if (string.IsNullOrWhiteSpace(parameterValue.ThrowIfNull(nameof(parameterName))))
                throw new ArgumentException("An argument cannot be empty.", parameterName);

            return parameterValue;
        }

        public static T ThrowIfLessThan<T>([NotNull] this T? parameterValue, T? value, [CallerArgumentExpression(nameof(parameterValue))] string parameterName = Empty) where T : IComparable<T>
        {
            if (parameterValue.ThrowIfNull(parameterName).CompareTo(value) < 0)
                throw new ArgumentOutOfRangeException(parameterName, parameterValue, $"An argument cannot less than {value}.");

            return parameterValue;
        }

        public static T ThrowIfNotOfType<T>([NotNull] this object? parameterValue, [CallerArgumentExpression(nameof(parameterValue))] string parameterName = Empty)
        {
            if (parameterValue.ThrowIfNull(parameterName) is not T value)
                throw new ArgumentException($"An argument cannot be cast to type {typeof(T)}.", parameterName);

            return value;
        }

        public static T ThrowIfNull<T>([NotNull] this T? parameterValue, [CallerArgumentExpression(nameof(parameterValue))] string parameterName = Empty)
        {
            if (parameterValue is null)
                throw new ArgumentNullException(parameterName, "An argument cannot be null.");

            return parameterValue;
        }
    }
}
