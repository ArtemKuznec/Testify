using Testify.Core.Interfaces;
using System;

namespace Testify.Core.Implementations.Requirements
{
    public sealed class WebComponentRequirement<TComponent> : WebComponentRequirementBuilder<TComponent, WebComponentRequirement<TComponent>> where TComponent : IWebComponent { }

    public sealed class WebComponentRequirement : WebComponentRequirementBuilder<IWebComponent, WebComponentRequirement> { }

    public abstract class WebComponentRequirementBuilder<TComponent, TBuilder> : RequirementBuilder<TComponent, TBuilder>
       where TBuilder : WebComponentRequirementBuilder<TComponent, TBuilder>
       where TComponent : IWebComponent
    {
        #region Is

        public virtual RequirementCombiner<TComponent, TBuilder> IsDisplayed(bool value = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.Properties.IsDisplayed(timeout),
                description: $"Is displayed",
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> IsReadOnly(bool value = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.Properties.IsReadOnly(timeout),
                description: $"Is read-only",
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> IsSelected(bool value = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.Properties.IsSelected(timeout),
                description: $"Is selected",
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> IsEnabled(bool value = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.Properties.IsEnabled(timeout),
                description: $"Is enabled",
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> IsAvailable(bool value = true)
        {
            var requirement = new Requirement<TComponent, bool>(
                getter: (component, timeout) => component.IsAvailable(timeout),
                description: $"Is avalable",
                expected: value);

            return CreateCombiner(requirement);
        }

        #endregion

        #region By

        public virtual RequirementCombiner<TComponent, TBuilder> ByAttribute(string name, string? value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var requirement = new Requirement<TComponent, string?>(
                getter: (component, timeout) => component.Properties.GetAttribute(name, timeout),
                description: $"Attribute \"{name}\"",
                comparer: string.Equals,
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> ByProperty(string name, string? value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var requirement = new Requirement<TComponent, string?>(
                getter: (component, timeout) => component.Properties.GetProperty(name, timeout),
                description: $"Property \"{name}\"",
                comparer: string.Equals,
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> ByValue(string? value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var requirement = new Requirement<TComponent, string?>(
                getter: (component, timeout) => component.Properties.GetValue(timeout),
                description: $"Value",
                comparer: string.Equals,
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> ByClass(string? value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var requirement = new Requirement<TComponent, string?>(
                getter: (component, timeout) => component.Properties.GetClass(timeout),
                description: $"Class",
                comparer: string.Equals,
                expected: value);

            return CreateCombiner(requirement);
        }

        public virtual RequirementCombiner<TComponent, TBuilder> ByText(string value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var requirement = new Requirement<TComponent, string?>(
                getter: (component, timeout) => component.Properties.GetText(timeout),
                description: $"Text",
                comparer: string.Equals,
                expected: value);

            return CreateCombiner(requirement);
        }

        #endregion
    }
}
