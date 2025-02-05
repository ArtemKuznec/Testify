using Testify.Core.Enums;
using Testify.Core.Implementations;
using Testify.Components.Interfaces;
using Testify.Components.Requirements;

namespace Testify.Components.Extensions
{
    public static class WebComponentBuilderExtensions
    {
        public static WebComponentBuilder<TComponent> ByText<TComponent>(this WebComponentBuilder<TComponent> builder, string text, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithText
        {
            var thisRequirement = new ComponentWithTextRequirement<TComponent>().ByText(text).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentBuilder<TComponent> ByValue<TComponent, TValue>(this WebComponentBuilder<TComponent> builder, TValue value, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithValue<TValue>
        {
            var thisRequirement = new ComponentWithValueRequirement<TComponent, TValue>().ByValue(value).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentBuilder<TComponent> ByPlaceholder<TComponent, TValue>(this WebComponentBuilder<TComponent> builder, TValue value, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithValue<TValue>
        {
            var thisRequirement = new ComponentWithValueRequirement<TComponent, TValue>().ByPlaceholder(value.ToString()).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentBuilder<TComponent> ByLabel<TComponent>(this WebComponentBuilder<TComponent> builder, string value, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithLabel
        {
            var thisRequirement = new ComponentWithLabelRequirement<TComponent>().ByLabel(value).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentBuilder<TComponent> HasText<TComponent>(this WebComponentBuilder<TComponent> builder, bool flag = true, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithText
        {
            var thisRequirement = new ComponentWithTextRequirement<TComponent>().HasText(flag).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentBuilder<TComponent> BySystemIdentifier<TComponent>(this WebComponentBuilder<TComponent> builder, string identifier, RequirementOperator @operator = RequirementOperator.And) where TComponent : IBaseComponent
        {
            var thisRequirement = new BaseRequirement<TComponent>().BySystemIdentifier(identifier).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentCollectionBuilder<TComponent> ByText<TComponent>(this WebComponentCollectionBuilder<TComponent> builder, string text, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithText
        {
            var thisRequirement = new ComponentWithTextRequirement<TComponent>().ByText(text).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentCollectionBuilder<TComponent> ByValue<TComponent, TValue>(this WebComponentCollectionBuilder<TComponent> builder, TValue value, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithValue<TValue>
        {
            var thisRequirement = new ComponentWithValueRequirement<TComponent, TValue>().ByValue(value).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentCollectionBuilder<TComponent> HasText<TComponent>(this WebComponentCollectionBuilder<TComponent> builder, bool flag = true, RequirementOperator @operator = RequirementOperator.And) where TComponent : IComponentWithText
        {
            var thisRequirement = new ComponentWithTextRequirement<TComponent>().HasText(flag).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }

        public static WebComponentCollectionBuilder<TComponent> BySystemIdentifier<TComponent>(this WebComponentCollectionBuilder<TComponent> builder, string identifier, RequirementOperator @operator = RequirementOperator.And) where TComponent : IBaseComponent
        {
            var thisRequirement = new BaseRequirement<TComponent>().BySystemIdentifier(identifier).Build();

            var builderRequirement = builder.Requirement;

            if (builderRequirement is null)
                return builder.WithRequirement(thisRequirement);

            return builder.WithRequirement(builderRequirement.Compose(@operator, thisRequirement));
        }
    }
}
