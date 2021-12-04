using System;

namespace GameElements
{
    public interface IGameSystem
    {
        event Action OnGameInitialized;
        
        event Action OnGameReady;
        
        event Action OnGameStarted;
        
        event Action OnGamePaused;

        event Action OnGameResumed;

        event Action OnGameFinished;
        
        GameState State { get; }

        void InitGame();

        void ReadyGame();

        void StartGame();

        void PauseGame();
        
        void ResumeGame();
    
        void FinishGame();

        void DisposeGame();
        
        void AddElement(IGameElement element);

        void RemoveElement(IGameElement element);

        bool RegisterService(object service);

        bool UnregisterService(object service);

        T GetService<T>();

        bool TryGetService<T>(out T service);
    }
}