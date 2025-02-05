using Testify.Components.Interfaces;
using Testify.Core.Implementations.Requirements;

namespace Testify.Components.Requirements
{
    public sealed class ComponentWithValueRequirement<TValue> : ComponentWithValueRequirementBuilder<IComponentWithValue<TValue>, ComponentWithValueRequirement<TValue>, TValue> { }


    public sealed class ComponentWithValueRequirement<TComponent, TValue> : ComponentWithValueRequirementBuilder<TComponent, ComponentWithValueRequirement<TComponent, TValue>, TValue> where TComponent : IComponentWithValue<TValue> { }

    public abstract class ComponentWithValueRequirementBuilder<TComponent, TBuilder, TValue> : BaseRequirementBuilder<TComponent, TBuilder>
        where TBuilder : ComponentWithValueRequirementBuilder<TComponent, TBuilder, TValue>
        where TComponent : IComponentWithValue<TValue>
    {
        private const string Description = "Value";

        private const string PlaceholderDescription = "Placeholder";

        public RequirementCombiner<TComponent, TBuilder> ByValue(TValue value)
        {
            var requirement = new Requirement<TComponent, TValue>(
                getter: (component, timeout) => component.GetValue(timeout),
                description: Description,
                expected: value);

            return CreateCombiner(requirement);
        }

        public RequirementCombiner<TComponent, TBuilder> ByPlaceholder(string value)
        {
            var requirement = new Requirement<TComponent, string>(
                getter: (component, timeout) => component.GetPlaceholder(timeout),
                description: PlaceholderDescription,
                expected: value);

            return CreateCombiner(requirement);
        }
    }
}
