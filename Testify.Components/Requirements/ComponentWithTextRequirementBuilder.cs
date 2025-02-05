using Testify.Core.Implementations.Requirements;
using Testify.Components.Interfaces;

namespace Testify.Components.Requirements
{
    public sealed class ComponentWithTextRequirement : ComponentWithTextRequirementBuilder<IComponentWithText, ComponentWithTextRequirement> { }

    public sealed class ComponentWithTextRequirement<TComponent> : ComponentWithTextRequirementBuilder<TComponent, ComponentWithTextRequirement<TComponent>> where TComponent : IComponentWithText { }

    public abstract class ComponentWithTextRequirementBuilder<TComponent, TBuilder> : BaseRequirementBuilder<TComponent, TBuilder>
        where TBuilder : ComponentWithTextRequirementBuilder<TComponent, TBuilder>
        where TComponent : IComponentWithText
    {
        private const string ByLabelDescription = "Label";

        private const string ByTextDescription = "Text";

        private const string HasTextDescription = "Has Text";

        public RequirementCombiner<TComponent, TBuilder> ByText(string text)
        {
            var requirement = new Requirement<TComponent, string>(
                getter: (component, timeout) => component.GetText(timeout),
                description: ByTextDescription,
                expected: text);

            return CreateCombiner(requirement);
        }

        public RequirementCombiner<TComponent, TBuilder> ByLabel(string text)
        {
            var requirement = new Requirement<TComponent, string>(
                getter: (component, timeout) => component.GetText(timeout),
                description: ByLabelDescription,
                expected: text);

            return CreateCombiner(requirement);
        }

        public RequirementCombiner<TComponent, TBuilder> HasText(bool flag = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.HasText(timeout),
                description: HasTextDescription,
                expected: flag);

            return CreateCombiner(requirement);
        }
    }
}
