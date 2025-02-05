using Testify.Core.Implementations;
using Testify.Components.Implementations.Abstractions;
using System;

namespace Testify.Components.Implementations.Primary
{
    public sealed class CheckBoxComponent : ComponentWithValue<bool>
    {
        private const string True = "true";

        private const string CheckedAttribute = "data-checked";

        public new static readonly Description DefaultDescription = new(Utilities.Signature("checkbox-wrapper"), "CheckBox Component");

        private static readonly Description _inputDescription = new(Utilities.Signature("checkbox-body"), "Input");

        protected override Description InitializeDescription() => DefaultDescription;

        protected override Description InitializeInputDescription() => _inputDescription;

        protected override bool Convert(string? value) => string.Equals(value, True);

        public override void SetValue(bool value, TimeSpan? timeout = null)
        {
            var current = GetValue(timeout);

            if (value != current)
                input.Actions.Click(timeout);
        }

        public override bool GetValue(TimeSpan? timeout = null)
        {
            var value = input.Properties.GetAttribute(CheckedAttribute, timeout);
            return Convert(value);
        }

        public override void Clear(TimeSpan? timeout = null)
        {
            if (GetValue(timeout))
                input.Actions.Click(timeout);
        }
    }
}
