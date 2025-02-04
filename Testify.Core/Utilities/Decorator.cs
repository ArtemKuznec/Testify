using Testify.Core.Enums;
using Testify.Core.Exceptions;
using Testify.Core.Implementations.Requirements;
using Testify.Core.Interfaces;
using System;

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

        public static TComponent Should<TComponent>(this TComponent component, Requirement<TComponent> requirement, TimeSpan? timeout = null) where TComponent : IWebComponent
        {
            Exception? catchException = null;

            try
            {
                if (requirement.Execute(component, timeout))
                    return component;
            }
            catch (Exception exception)
            {
                catchException = exception;
            }

            throw new RequirementException($"The web component could not meet the \"{requirement}\".", component, catchException);
        }

        public static bool Has<TComponent>(this TComponent component, Requirement<TComponent> requirement, TimeSpan? timeout = null) where TComponent : IWebComponent
        {
            try
            {
                return requirement.Execute(component, timeout);
            }
            catch
            {
                return false;
            }
        }
    }
}
