using Testify.Core.Enums;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;

namespace Testify.Core.Implementations.Requirements
{
    public abstract class RequirementBuilder<TComponent, TBuilder> where TComponent : IWebComponent where TBuilder : RequirementBuilder<TComponent, TBuilder>
    {
        private Requirement<TComponent>? _requirement = null;

        private RequirementOperator _operator = RequirementOperator.None;

        protected virtual RequirementCombiner<TComponent, TBuilder> CreateCombiner(Requirement<TComponent> requirement)
        {
            if (_requirement is null)
                return new RequirementCombiner<TComponent, TBuilder>(requirement);

            requirement = _requirement.Compose(_operator, requirement);
            return new RequirementCombiner<TComponent, TBuilder>(requirement);
        }

        internal void SetOperator(RequirementOperator @operator) => _operator = @operator;

        internal void SetRequirement(Requirement<TComponent> requirement) => _requirement = requirement.ThrowIfNull();
    }
}
