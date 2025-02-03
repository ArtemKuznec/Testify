using Testify.Core.Enums;
using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Testify.Core.Implementations.Requirements
{
    public sealed class CompositeRequirement<TComponent> : Requirement<TComponent> where TComponent : IWebComponent
    {
        public IReadOnlyList<Requirement<TComponent>> Requirements { get; }

        public CompositeRequirement(Requirement<TComponent> left, RequirementOperator @operator, Requirement<TComponent> right) : base()
        {
            static void Add(List<Requirement<TComponent>> requirements, Requirement<TComponent> requirement, RequirementOperator @operator)
            {
                if (requirement is CompositeRequirement<TComponent> composite)
                {
                    if (composite.HasParentheses)
                    {
                        requirements.Add(composite.WithOperator(@operator));
                    }
                    else
                    {
                        requirements.Add(composite.Requirements[0].WithOperator(@operator));
                        requirements.AddRange(composite.Requirements.Skip(1));
                    }
                }
                else requirements.Add(requirement.WithOperator(@operator));
            }

            if (@operator == RequirementOperator.None)
                throw new Exception(); // TODO: Изменить сообщение.

            var requirements = new List<Requirement<TComponent>>();

            Add(requirements, left.ThrowIfNull(), RequirementOperator.None);
            Add(requirements, right.ThrowIfNull(), @operator);

            if (requirements.Count < 2)
                throw new Exception(); // TODO: Изменить сообщение.

            Requirements = requirements;
        }

        private CompositeRequirement(CompositeRequirement<TComponent> requirement, RequirementOperator @operator, bool hasParentheses, bool hasInversion)
            : base(@operator, hasParentheses, hasInversion) => Requirements = requirement.Requirements;

        public override Requirement<TComponent> WithOperator(RequirementOperator @operator = RequirementOperator.None) => new CompositeRequirement<TComponent>(this, @operator, HasParentheses, HasInversion);

        public override Requirement<TComponent> WithParentheses(bool value = true) => new CompositeRequirement<TComponent>(this, Operator, HasInversion || value, HasInversion);

        public override Requirement<TComponent> WithInversion(bool value = true) => new CompositeRequirement<TComponent>(this, Operator, value || HasParentheses, value);

        public override bool Execute(TComponent component, TimeSpan? timeout = null)
        {
            try
            {
                var requirement = Requirements[0];
                var result = requirement.Execute(component, timeout);

                for (var index = 1; index < Requirements.Count; index++)
                {
                    requirement = Requirements[index];

                    switch (requirement.Operator)
                    {
                        case RequirementOperator.And:
                            {
                                if (result == HasInversion)
                                    continue;

                                result &= requirement.Execute(component, timeout);

                                break;
                            }
                        case RequirementOperator.Or:
                            {
                                if (result != HasInversion)
                                    return true;

                                result |= requirement.Execute(component, timeout);

                                break;
                            }
                        default:
                            {
                                throw new Exception(); // TODO: Изменить сообщение.
                            }
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Empty, exception); // TODO: Изменить сообщение.
            }
        }

        public bool Equals([NotNullWhen(true)] CompositeRequirement<TComponent>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Equals(other as Requirement<TComponent>);
            result &= Requirements.SequenceEqual(other.Requirements);

            return result;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as CompositeRequirement<TComponent>);

        public override int GetHashCode()
        {
            var builder = new HashCode();

            builder.Add(base.GetHashCode());

            for (var index = 0; index < Requirements.Count; index++)
                builder.Add(Requirements[index]);

            return builder.ToHashCode();
        }

        public override string ToString()
        {
            var hasOperator = Operator != RequirementOperator.None;
            var result = string.Join(" ", Requirements.Select(requirement => requirement.ToString()));

            if (HasParentheses || HasInversion || hasOperator)
            {
                if (HasInversion) result = $"NOT ({result})";
                else result = $"({result})";
            }

            if (hasOperator)
            {
                var @operator = Operator.ToString().ToUpper();
                result = $"{@operator} {result}";
            }

            return result;
        }
    }
}
