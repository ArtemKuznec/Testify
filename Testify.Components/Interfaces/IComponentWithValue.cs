namespace Testify.Components.Interfaces
{
    public interface IComponentWithValue<TValue> : IBaseComponent
    {
        void SetValue(TValue value, TimeSpan? timeout = null);

        TValue GetValue(TimeSpan? timeout = null);

        void Clear(TimeSpan? timeout = null);

        string GetPlaceholder(TimeSpan? timeout = null);

        void SendKeys(string keys, TimeSpan? timeout = null);
    }
}
