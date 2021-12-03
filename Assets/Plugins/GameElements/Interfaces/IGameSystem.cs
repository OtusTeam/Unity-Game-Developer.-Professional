using System;

namespace GameElements
{
    public interface IGameSystem
    {
        event Action OnGameInitialize;
        
        event Action OnGameReady;
        
        event Action OnGameStart;
        
        event Action OnGamePause;

        event Action OnGameResume;

        event Action OnGameFinish;
        
        GameState State { get; }

        void InitializeGame();

        void ReadyGame();

        void StartGame();

        void PauseGame();
        
        void ResumeGame();
    
        void FinishGame();

        void DestroyGame();

        bool AddService(object service);

        bool RemoveService(object service);

        T GetService<T>();

        bool TryGetService<T>(out T service);
    }
}