using Testify.Core.Implementations.Requirements;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Testify.Core.Implementations
{
    public sealed class Condition<TComponent> : IEquatable<Condition<TComponent>>, ICondition where TComponent : IWebComponent
    {
        public bool Enabled { get; set; }

        private readonly TComponent _component;

        private readonly Requirement<TComponent> _requirement;

        public Condition(TComponent component, Requirement<TComponent> requirement, bool enabled = true)
        {
            _requirement = requirement.ThrowIfNull();
            _component = component.ThrowIfNull();

            Enabled = enabled;
        }

        public bool Execute(TimeSpan? timeout = null) => _requirement.Execute(_component, timeout);

        public bool Equals([NotNullWhen(true)] Condition<TComponent>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Enabled == other.Enabled;
            result &= Equals(_requirement, other._requirement);

            return result;
        }

        public override bool Equals(object? obj) => Equals(obj as Condition<TComponent>);

        public override int GetHashCode() => HashCode.Combine(Enabled, _requirement);

        public override string? ToString() => _requirement.ToString();
    }
}
