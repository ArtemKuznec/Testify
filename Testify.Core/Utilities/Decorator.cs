using Testify.Core.Enums;
using Testify.Core.Implementations.Requirements;
using Testify.Core.Interfaces;

namespace Testify.Core.Utilities
{
    public static class Decorator
    {
        public static TBuilder And<TComponent, TBuilder>(this RequirementCombiner<TComponent, TBuilder> combiner) where TComponent : IWebComponent where TBuilder : RequirementBuilder<TComponent, TBuilder>
        {
            return combiner.Combine(RequirementOperator.And);
        }

        public static TBuilder Or<TComponent, TBuilder>(this RequirementCombiner<TComponent, TBuilder> combiner) where TComponent : IWebComponent where TBuilder : RequirementBuilder<TComponent, TBuilder>
        {
            return combiner.Combine(RequirementOperator.Or);
        }

        public static CompositeRequirement<TComponent> And<TComponent>(this Requirement<TComponent> left, Requirement<TComponent> right) where TComponent : IWebComponent
        {
            return left.Compose(RequirementOperator.And, right);
        }

        public static CompositeRequirement<TComponent> Or<TComponent>(this Requirement<TComponent> left, Requirement<TComponent> right) where TComponent : IWebComponent
        {
            return left.Compose(RequirementOperator.Or, right);
        }
    }
}
