namespace Prototype.GameInterface
{
    public interface IFactory<out T>
    {
        T Instantiate();
    }
}