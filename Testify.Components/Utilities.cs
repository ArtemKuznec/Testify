using Testify.Core.Implementations;

namespace Testify.Components
{
    internal static class Utilities
    {
        public static Selector Signature(string pattern) => Selector.Css($"*[data-signature='{pattern}']");
    }
}
