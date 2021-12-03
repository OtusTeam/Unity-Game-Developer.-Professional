namespace GameElements
{
    public interface IGameElement
    {
        void BindGame(IGameSystem system);

        void UnbindGame();
    }
}