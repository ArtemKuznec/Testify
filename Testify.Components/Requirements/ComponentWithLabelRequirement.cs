using Testify.Core.Implementations.Requirements;
using Testify.Components.Interfaces;

namespace Testify.Components.Requirements
{

    public sealed class ComponentWithLabelRequirement : ComponentWithLabelRequirementBuilder<IComponentWithLabel, ComponentWithLabelRequirement> { }

    public sealed class ComponentWithLabelRequirement<TComponent> : ComponentWithLabelRequirementBuilder<TComponent, ComponentWithLabelRequirement<TComponent>> where TComponent : IComponentWithLabel { }

    public abstract class ComponentWithLabelRequirementBuilder<TComponent, TBuilder> : BaseRequirementBuilder<TComponent, TBuilder>
       where TBuilder : ComponentWithLabelRequirementBuilder<TComponent, TBuilder>
       where TComponent : IComponentWithLabel
    {
        private const string ByLabelDescription = "Label";

        private const string HasLabelDescription = "Has label";

        public RequirementCombiner<TComponent, TBuilder> ByLabel(string text)
        {
            var requirement = new Requirement<TComponent, string>(
                getter: (component, timeout) => component.GetLabel(timeout),
                description: ByLabelDescription,
                expected: text);

            return CreateCombiner(requirement);
        }

        public RequirementCombiner<TComponent, TBuilder> HasLabel(bool flag = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.HasLabel(timeout),
                description: HasLabelDescription,
                expected: flag);

            return CreateCombiner(requirement);
        }
    }
}
