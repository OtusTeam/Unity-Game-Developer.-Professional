namespace Prototype
{
    public interface IFactory<out T>
    {
        T Instantiate();
    }
}