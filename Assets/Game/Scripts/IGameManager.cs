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
    }
}