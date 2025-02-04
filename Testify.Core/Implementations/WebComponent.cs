using Testify.Core.Interfaces;
using Testify.Core.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Testify.Core.Implementations
{
    public class WebComponent : IWebComponent
    {
        public Description Description { get; private set; }

        public Properties Properties { get; }

        public Refresher Refresher { get; }

        public Actions Actions { get; }

        public IWebComponent? Parent { get; private set; }

        public ICondition? Condition { get; private set; }

        public TimeSpan Timeout { get; private set; }

        public int Index { get; private set; }

        protected WebComponent()
        {
            Actions = IWebComponent.Configuration.CreateActions(this).ThrowIfNull();
            Description = InitializeDescription().ThrowIfNull();
            Properties = new(this);
            Refresher = new(this);
        }

        protected virtual Description InitializeDescription() => new(Selector.Css("*"), "Web Component");

        public virtual WebComponentCollectionBuilder<TComponent> GetComponents<TComponent>() where TComponent : IWebComponent => new(this);

        public virtual WebComponentBuilder<TComponent> GetComponent<TComponent>() where TComponent : IWebComponent => new(this);

        public virtual WebComponentCollectionBuilder<IWebComponent> GetComponents() => new WebComponentCollectionBuilder<IWebComponent>(this).WithType<WebComponent>();

        public virtual WebComponentBuilder<IWebComponent> GetComponent() => new WebComponentBuilder<IWebComponent>(this).WithType<WebComponent>();

        public bool IsAvailable(TimeSpan? timeout = null)
        {
            using var source = new CancellationTokenSource();

            timeout ??= Timeout;

            try
            {
                Refresher.Refresh(source.Token, timeout.Value);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                source.Cancel();
            }
        }

        public bool Equals([NotNullWhen(true)] IWebComponent? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            var result = Description == other.Description;

            result &= Index == other.Index;
            result &= Equals(Parent, other.Parent);
            result &= Equals(Condition, other.Condition);

            return result;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj as IWebComponent);

        public override int GetHashCode() => HashCode.Combine(Description, Parent, Condition, Index);

        public override string ToString()
        {
            int indexValue;

            if (Condition is null || Condition.Enabled)
                indexValue = Description.Index;
            else indexValue = Index;

            var index = indexValue > 0 ? $"[{indexValue}]" : string.Empty;
            var condition = Condition?.Enabled == true ? $"{Condition}" : string.Empty;

            var result = $"{Description.Name}{index} {condition}";

            if (Parent is null)
                return result;

            return $"{Parent} -> {result}";
        }

        void IWebComponent.SetCondition(ICondition condition) => Condition = condition.ThrowIfNull();

        void IWebComponent.SetDescription(Description description) => Description = description.ThrowIfNull();

        void IWebComponent.SetIndex(int index) => Index = index.ThrowIfLessThan(0);

        void IWebComponent.SetParent(IWebComponent parent) => Parent = parent.ThrowIfNull();

        void IWebComponent.SetTimeout(TimeSpan timeout) => Timeout = timeout;
    }
}
