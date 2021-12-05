namespace Prototype
{
    public interface IPool<T>
    {
        T Pop();

        void Push(T value);
    }
}