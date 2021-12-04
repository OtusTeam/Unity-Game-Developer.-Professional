namespace GameElements
{
    public interface IGameElementContext
    {
        void AddElement(IGameElement element);

        void RemoveElement(IGameElement element);

        void InitGame();

        void ReadyGame();

        void StartGame();

        void PauseGame();

        void ResumeGame();

        void FinishGame();
    }
}