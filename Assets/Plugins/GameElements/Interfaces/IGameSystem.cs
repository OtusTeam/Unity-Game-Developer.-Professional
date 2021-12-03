using System;

namespace GameElements
{
    public interface IGameSystem
    {
        GameState State { get; }
        
        event Action OnGameInitialize;
        
        event Action OnGameReady;
        
        event Action OnGameStart;
        
        event Action OnGamePause;

        event Action OnGameResume;

        event Action OnGameFinish;

        void InitializeGame();

        void ReadyGame();

        void StartGame();

        void PauseGame();
        
        void ResumeGame();
    
        void FinishGame();

        void DestroyGame();

        bool AddElement(object element);

        bool RemoveElement(object element);

        T GetElement<T>();

        bool TryGetElement<T>(out T element);
    }
}