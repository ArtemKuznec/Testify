using Testify.Core.Extensions;
using Testify.Core.Implementations.Requirements;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;

namespace Testify.Core.Implementations
{
    public sealed class WebComponentBuilder<TComponent> where TComponent : IWebComponent
    {
        public Requirement<TComponent>? Requirement { get; private set; }

        private readonly IWebComponent? _parent;

        private Description? _description;

        private TimeSpan _timeout;

        private Type? _type;

        internal WebComponentBuilder(IWebComponent? parent)
        {
            _timeout = IWebComponent.Configuration.Timeout;
            _parent = parent;
        }

        private WebComponentBuilder(WebComponentBuilder<TComponent> builder)
        {
            Requirement = builder.Requirement;
            _description = builder._description;
            _timeout = builder._timeout;
            _parent = builder._parent;
            _type = builder._type;
        }

        public WebComponentBuilder<TComponent> WithRequirement<TBuilder>(Func<TBuilder, Requirement<TComponent>?> requirement) where TBuilder : RequirementBuilder<TComponent, TBuilder>
        {
            var builder = TypeExtensions.CreateInstance<TBuilder>(null);
            return new(this) { Requirement = requirement.ThrowIfNull().Invoke(builder) };
        }

        public WebComponentBuilder<TComponent> WithRequirement(Requirement<TComponent>? requirement) => new(this) { Requirement = requirement };

        public WebComponentBuilder<TComponent> WithDescription(Description? description) => new(this) { _description = description };

        public WebComponentBuilder<TComponent> WithType<T>() where T : TComponent => new(this) { _type = typeof(T) };

        public WebComponentBuilder<TComponent> WithTimeout(TimeSpan timeout) => new(this) { _timeout = timeout };

        public WebComponentBuilder<TComponent> WithType(Type type) => new(this) { _type = type };

        public TComponent Build()
        {
            var component = TypeExtensions.CreateInstance<TComponent>(_type);

            if (Requirement is not null)
            {
                var condition = new Condition<TComponent>(component, Requirement);
                component.SetCondition(condition);
            }

            if (_description is not null)
                component.SetDescription(_description);

            if (_parent is not null)
                component.SetParent(_parent);

            component.SetTimeout(_timeout);

            return component;
        }
    }
}
