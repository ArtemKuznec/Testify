using Testify.Core.Extensions;
using Testify.Core.Implementations.Requirements;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Testify.Core.Implementations
{
    public sealed class WebComponentCollectionBuilder<TComponent> where TComponent : IWebComponent
    {
        public Requirement<TComponent>? Requirement { get; private set; }

        private readonly IWebComponent? _parent;

        private Description? _description;

        private TimeSpan _timeout;

        private TimeSpan _duration;

        private Type _type;

        internal WebComponentCollectionBuilder(IWebComponent? parent)
        {
            _duration = IWebComponent.Configuration.Duration;
            _timeout = IWebComponent.Configuration.Timeout;
            _type = typeof(TComponent);
            _parent = parent;
        }

        private WebComponentCollectionBuilder(WebComponentCollectionBuilder<TComponent> builder)
        {
            Requirement = builder.Requirement;
            _description = builder._description;
            _duration = builder._duration;
            _timeout = builder._timeout;
            _parent = builder._parent;
            _type = builder._type;
        }

        public WebComponentCollectionBuilder<TComponent> WithRequirement<TBuilder>(Func<TBuilder, Requirement<TComponent>?> requirement) where TBuilder : RequirementBuilder<TComponent, TBuilder>
        {
            var builder = TypeExtensions.CreateInstance<TBuilder>(null);
            return new(this) { Requirement = requirement.ThrowIfNull().Invoke(builder) };
        }

        public WebComponentCollectionBuilder<TComponent> WithRequirement(Requirement<TComponent>? requirement) => new(this) { Requirement = requirement };

        public WebComponentCollectionBuilder<TComponent> WithDescription(Description? description) => new(this) { _description = description };

        public WebComponentCollectionBuilder<TComponent> WithType<T>() where T : class, TComponent => new(this) { _type = typeof(T) };

        public WebComponentCollectionBuilder<TComponent> WithDuration(TimeSpan duration) => new(this) { _duration = duration };

        public WebComponentCollectionBuilder<TComponent> WithTimeout(TimeSpan timeout) => new(this) { _timeout = timeout };

        public WebComponentCollectionBuilder<TComponent> WithType(Type type) => new(this) { _type = type };

        public IReadOnlyList<TComponent> Build() => Build(null);

        private IReadOnlyList<TComponent> Build(List<TComponent>? components)
        {
            components ??= new List<TComponent>();

            var builder = _parent is not null
                ? _parent.GetComponent<TComponent>()
                : IWebComponent.Configuration.Context.GetComponent<TComponent>();

            builder = builder
                .WithDescription(_description)
                .WithTimeout(_timeout)
                .WithType(_type);

            var instance = builder.Build();

            var count = 0;

            while (true)
            {
                var description = instance.Description.With(count);
                instance.SetDescription(description);

                if (instance.IsAvailable(_duration)) count++;
                else break;
            }

            if (Requirement is null)
            {
                for (var index = 0; index < count; index++)
                {
                    var description = instance.Description.With(index);
                    var component = builder.WithDescription(description).Build();
                    components.Add(component);
                }
            }
            else
            {
                var indexes = components.Select(component => component.Index).ToArray();

                for (int index = 0; index < count; index++)
                {
                    if (indexes.Contains(index))
                        continue;

                    var description = instance.Description.With(index);
                    instance.SetDescription(description);

                    if (Requirement.Execute(instance, _duration))
                    {
                        description = description.With(index);
                        var component = builder.WithDescription(description).Build();
                        components.Add(component);
                    }
                }
            }

            var availableRequirement = Requirement is not null ? Requirement
                : new WebComponentRequirement<TComponent>().IsAvailable().Build();

            var availableComponents = components.Where(component => availableRequirement.Execute(component, TimeSpan.Zero)).ToList();

            if (components.Count != availableComponents.Count)
                return Build(availableComponents);

            if (Requirement is not null)
            {
                components.Sort((left, right) => left.Index.CompareTo(right.Index));

                for (var index = 0; index < components.Count; index++)
                {
                    var description = components[index].Description.With(index);
                    var condition = new Condition<TComponent>(components[index], Requirement);

                    components[index].SetDescription(description);
                    components[index].SetCondition(condition);
                }
            }

            return components;
        }
    }
}
