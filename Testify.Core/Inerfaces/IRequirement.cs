using Testify.Core.Enums;

namespace Testify.Core.Inerfaces
{
    public interface IRequirement<TComponent> where TComponent : IWebComponent
    {
        RequirementOperator Operator { get; }

        bool HasParentheses { get; }

        bool HasInversion { get; }

        ICompositeRequirement<TComponent> Compose(RequirementOperator @operator, IRequirement<TComponent> requirement);

        IRequirement<TComponent> WithOperator(RequirementOperator @operator = RequirementOperator.None);

        IRequirement<TComponent> WithParentheses(bool value = true);

        IRequirement<TComponent> WithInversion(bool value = true);

        bool Execute(TComponent component, TimeSpan? timeout = null);
    }

}
