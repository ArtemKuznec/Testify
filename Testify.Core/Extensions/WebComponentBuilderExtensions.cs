using Testify.Core.Implementations;
using Testify.Core.Interfaces;
using System;

namespace Testify.Core.Extensions
{
    internal static class WebComponentBuilderExtensions
    {
        public static WebComponentBuilder<TComponent> WithType<TComponent>(this WebComponentBuilder<TComponent> builder, Type type) where TComponent : IWebComponent
        {
            return builder.GetType().GetMethod(nameof(WebComponentBuilder<IWebComponent>.WithType))?
                .MakeGenericMethod(type).Invoke(builder, Array.Empty<object>()) as WebComponentBuilder<TComponent>
                ?? throw new Exception(); // TODO: Разобраться с мусором.
        }
    }
}
