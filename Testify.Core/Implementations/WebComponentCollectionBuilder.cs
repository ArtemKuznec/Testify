using Testify.Core.Extensions;
using Testify.Core.Implementations.Requirements;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Testify.Core.Implementations
{
    public sealed class WebComponentCollectionBuilder<TComponent> where TComponent : IWebComponent
    {
        private readonly IWebComponent? _parent;

        private Requirement<TComponent>? _requirement;

        private Description? _description;

        private TimeSpan? _timeout;

        private TimeSpan _duration;

        private Type _type;

        internal WebComponentCollectionBuilder(IWebComponent? parent)
        {
            _duration = TimeSpan.FromMilliseconds(500);
            _type = typeof(TComponent);
            _parent = parent;
        }

        private WebComponentCollectionBuilder(WebComponentCollectionBuilder<TComponent> builder)
        {
            _requirement = builder._requirement;
            _description = builder._description;
            _duration = builder._duration;
            _timeout = builder._timeout;
            _parent = builder._parent;
            _type = builder._type;
        }

        public WebComponentCollectionBuilder<TComponent> WithRequirement<TBuilder>(Func<TBuilder, Requirement<TComponent>?> requirement) where TBuilder : RequirementBuilder<TComponent, TBuilder>
        {
            var builder = TypeExtensions.CreateInstance<TBuilder>(null);
            return new(this) { _requirement = requirement.ThrowIfNull().Invoke(builder) };
        }

        public WebComponentCollectionBuilder<TComponent> WithRequirement(Requirement<TComponent>? requirement) => new(this) { _requirement = requirement };

        public WebComponentCollectionBuilder<TComponent> WithDescription(Description? description) => new(this) { _description = description };

        public WebComponentCollectionBuilder<TComponent> WithType<T>() where T : class, TComponent => new(this) { _type = typeof(T) };

        public WebComponentCollectionBuilder<TComponent> WithDuration(TimeSpan duration) => new(this) { _duration = duration };

        public WebComponentCollectionBuilder<TComponent> WithTimeout(TimeSpan? timeout) => new(this) { _timeout = timeout };

        public IReadOnlyList<TComponent> Build() => Build(null);

        private IReadOnlyList<TComponent> Build(List<TComponent>? components)
        {
            using var source = new CancellationTokenSource();

            components ??= new List<TComponent>();

            try
            {
                var builder = _parent is not null
                    ? _parent.GetComponent<TComponent>()
                    : IWebComponent.Configuration.Context.GetComponent<TComponent>();

                builder = builder.WithType(_type);

                var instance = builder
                    .WithDescription(_description)
                    .WithTimeout(_timeout)
                    .Build();

                if (_requirement is null)
                {
                    while (true)
                    {
                        var description = instance.Description.With(components.Count);
                        var component = builder.WithDescription(description).Build();

                        if (component.IsAvailable(_duration))
                            components.Add(component);
                        else break;
                    }
                }
                else
                {
                    var indexes = components.Select(component => component.Index).ToArray();

                    for (int index = 0; index < int.MaxValue; index++)
                    {
                        if (indexes.Contains(index))
                            continue;

                        var description = instance.Description.With(index);
                        instance.SetDescription(description);

                        if (_requirement.Execute(instance, _duration))
                        {
                            description = description.With(components.Count);
                            var component = builder.WithDescription(description).Build();

                            component.SetIndex(index);
                            components.Add(component);
                        }
                        else break;
                    }
                }

                var availableRequirement = _requirement is not null ? _requirement
                    : new WebComponentRequirement<TComponent>().IsAvailable().Build();

                var availableComponents = components.Where(component => availableRequirement.Execute(component, TimeSpan.Zero)).ToList();

                if (components.Count != availableComponents.Count)
                    return Build(availableComponents);

                if (_requirement is not null)
                {
                    components.Sort((left, right) => left.Index.CompareTo(right.Index));

                    for (var index = 0; index < components.Count; index++)
                    {
                        var description = components[index].Description.With(index);
                        var condition = new Condition<TComponent>(components[index], _requirement);

                        components[index].SetDescription(description);
                        components[index].SetCondition(condition);
                    }
                }

                return components;
            }
            finally
            {
                source.Cancel();
            }
        }
    }
}
