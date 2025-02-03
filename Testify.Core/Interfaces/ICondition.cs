
namespace Testify.Core.Interfaces
{
    public interface ICondition
    {
        bool Enabled { get; set; }

        bool Execute(TimeSpan? timeout = null);
    }
}
