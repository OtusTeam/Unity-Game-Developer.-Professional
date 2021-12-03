namespace Prototype.GameInterface
{
    public interface IPool<T>
    {
        T Pop();

        void Push(T value);
    }
}