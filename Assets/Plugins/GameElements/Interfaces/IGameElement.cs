namespace GameElements
{
    public interface IGameElement
    {
        void Setup(IGameSystem system);

        void Dispose();
    }
}