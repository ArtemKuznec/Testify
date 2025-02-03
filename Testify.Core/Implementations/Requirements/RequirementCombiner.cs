using Testify.Core.Enums;
using Testify.Core.Extensions;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;

namespace Testify.Core.Implementations.Requirements
{
    public sealed class RequirementCombiner<TComponent, TBuilder> where TComponent : IWebComponent where TBuilder : RequirementBuilder<TComponent, TBuilder>
    {
        private readonly Requirement<TComponent> _requirement;

        public RequirementCombiner(Requirement<TComponent> requirement) => _requirement = requirement.ThrowIfNull();

        public RequirementCombiner<TComponent, TBuilder> WithParentheses(bool value = true)
        {
            var requirement = _requirement.WithParentheses(value);
            return new RequirementCombiner<TComponent, TBuilder>(requirement);
        }

        public RequirementCombiner<TComponent, TBuilder> WithInversion(bool value = true)
        {
            var requirement = _requirement.WithInversion(value);
            return new RequirementCombiner<TComponent, TBuilder>(requirement);
        }

        public RequirementCombiner<TComponent, TBuilder> Combine(RequirementOperator @operator, Requirement<TComponent> requirement)
        {
            requirement = _requirement.Compose(@operator, requirement);
            return new RequirementCombiner<TComponent, TBuilder>(requirement);
        }

        public TBuilder Combine(RequirementOperator @operator)
        {
            var builder = TypeExtensions.CreateInstance<TBuilder>(null);

            builder.SetRequirement(_requirement);
            builder.SetOperator(@operator);

            return builder;
        }

        public Requirement<TComponent> Build() => _requirement;

        public override string? ToString() => _requirement.ToString();
    }
}
