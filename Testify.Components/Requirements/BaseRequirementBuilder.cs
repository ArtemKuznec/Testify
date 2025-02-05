using Testify.Components.Interfaces;
using Testify.Core.Implementations.Requirements;

namespace Testify.Components.Requirements
{
    public sealed class BaseRequirement : BaseRequirementBuilder<IBaseComponent, BaseRequirement> { }

    public sealed class BaseRequirement<TComponent> : BaseRequirementBuilder<TComponent, BaseRequirement<TComponent>> where TComponent : IBaseComponent { }

    public abstract class BaseRequirementBuilder<TComponent, TBuilder> : RequirementBuilder<TComponent, TBuilder>
        where TBuilder : BaseRequirementBuilder<TComponent, TBuilder>
        where TComponent : IBaseComponent
    {
        private const string Description = "System Identifier";

        public RequirementCombiner<TComponent, TBuilder> BySystemIdentifier(string identifier)
        {
            var requirement = new Requirement<TComponent, string>(
                getter: (component, timeout) => component.GetSystemIdentifier(timeout),
                description: Description,
                expected: identifier);

            return CreateCombiner(requirement);
        }
    }
}
