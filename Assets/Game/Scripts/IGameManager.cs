using System;

namespace Otus
{
    public interface IGameManager
    {
        event Action OnInitializeGame;
        
        event Action OnStartGame;
        
        event Action OnFinishGame;
        
        void InitializeGame();
        
        void StartGame();
        
        void FinishGame();

        void AddUpdateListener(IUpdateListener listener);

        void RemoveUpdateListener(IUpdateListener listener);
        
        void AddFixedUpdateListener(IUpdateListener listener);
        
        void RemoveFixedUpdateListener(IUpdateListener listener);
    }
}