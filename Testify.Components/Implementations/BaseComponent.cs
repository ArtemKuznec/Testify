using Testify.Core.Implementations;
using Testify.Components.Interfaces;
using System;

namespace Testify.Components.Implementations
{
    public class BaseComponent : WebComponent, IBaseComponent
    {
        private const string SystemIdentifierAttribute = "data-sysid";

        public static readonly Description DefaultDescription = new(Selector.Css("*"), "Base Component");

        protected override Description InitializeDescription() => DefaultDescription;

        public string GetSystemIdentifier(TimeSpan? timeout = null) => Properties.GetAttribute(SystemIdentifierAttribute, timeout) ?? string.Empty;
    }
}
