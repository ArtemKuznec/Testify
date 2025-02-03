using Testify.Core.Enums;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Testify.Core.Implementations.Requirements
{
    public abstract class Requirement<TComponent> where TComponent : IWebComponent
    {
        public RequirementOperator Operator { get; }

        public bool HasParentheses { get; }

        public bool HasInversion { get; }

        protected Requirement(RequirementOperator @operator = RequirementOperator.None, bool hasParentheses = false, bool hasInversion = false)
        {
            HasParentheses = hasParentheses;
            HasInversion = hasInversion;
            Operator = @operator;
        }

        public virtual CompositeRequirement<TComponent> Compose(RequirementOperator @operator, Requirement<TComponent> requirement) => new(this, @operator, requirement);

        public abstract Requirement<TComponent> WithOperator(RequirementOperator @operator = RequirementOperator.None);

        public abstract Requirement<TComponent> WithParentheses(bool value = true);

        public abstract Requirement<TComponent> WithInversion(bool value = true);

        public abstract bool Execute(TComponent component, TimeSpan? timeout = null);

        public bool Equals([NotNullWhen(true)] Requirement<TComponent>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Operator == other.Operator;

            result &= HasParentheses == other.HasParentheses;
            result &= HasInversion == other.HasInversion;

            return result;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as Requirement<TComponent>);

        public override int GetHashCode() => HashCode.Combine(Operator, HasParentheses, HasInversion);
    }

    public sealed class Requirement<TComponent, TValue> : Requirement<TComponent> where TComponent : IWebComponent
    {
        public string Description { get; }

        public TValue Expected { get; }

        private readonly Func<TValue, TValue, bool> _comparer;

        private readonly Func<TComponent, TimeSpan?, TValue> _getter;

        public Requirement(Func<TComponent, TimeSpan?, TValue> getter, TValue expected, string description, Func<TValue, TValue, bool>? comparer = null) : base()
        {
            _comparer = comparer ?? ((left, right) => Equals(left, right));
            Description = description.ThrowIfNullOrWhiteSpace();
            _getter = getter.ThrowIfNull();
            Expected = expected;
        }

        private Requirement(Requirement<TComponent, TValue> requirement, RequirementOperator @operator, bool hasParentheses, bool hasInversion) : base(@operator, hasParentheses, hasInversion)
        {
            Description = requirement.Description;
            Expected = requirement.Expected;
            _comparer = requirement._comparer;
            _getter = requirement._getter;
        }

        public override Requirement<TComponent> WithOperator(RequirementOperator @operator = RequirementOperator.None) => new Requirement<TComponent, TValue>(this, @operator, HasParentheses, HasInversion);

        public override Requirement<TComponent> WithParentheses(bool value = true) => new Requirement<TComponent, TValue>(this, Operator, value, HasInversion);

        public override Requirement<TComponent> WithInversion(bool value = true) => new Requirement<TComponent, TValue>(this, Operator, HasParentheses, value);

        public override bool Execute(TComponent component, TimeSpan? timeout = null)
        {
            try
            {
                TValue actual;
                bool result;

                try { actual = _getter.Invoke(component, timeout); }
                catch (Exception exception)
                {
                    throw new Exception(string.Empty, exception); // TODO: Изменить сообщение.
                }

                try { result = _comparer.Invoke(actual, Expected); }
                catch (Exception exception)
                {
                    throw new Exception(string.Empty, exception); // TODO: Изменить сообщение.
                }

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Empty, exception); // TODO: Изменить сообщение.
            }
        }

        public bool Equals([NotNullWhen(true)] Requirement<TComponent, TValue>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Equals(other as Requirement<TComponent>);

            result &= Description == other.Description;
            result &= Equals(Expected, other.Expected);

            return result;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as Requirement<TComponent, TValue>);

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Description, Expected);

        public override string ToString()
        {
            var result = $"[{Description} {(HasInversion ? "!=" : "==")} {Expected}]";

            if (Operator != RequirementOperator.None)
            {
                var @operator = Operator.ToString().ToUpper();
                result = $"{@operator} {result}";
            }

            return result;
        }
    }
}
