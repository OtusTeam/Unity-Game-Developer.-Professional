namespace DynamicObjects
{
    public interface IMethodArgs
    {
        T GetParameter<T>(object key);

        bool TryGetParameter<T>(object key, out T value);
    }
}