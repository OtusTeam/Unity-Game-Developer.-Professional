using System;

namespace GameElements
{
    public abstract class GameController : IGameElement
    {
        private IGameSystem gameSystem;
        
        #region Lifecycle

        void IGameElement.BindGame(IGameSystem system)
        {
            if (this.gameSystem != null)
            {
                throw new Exception($"{this.GetType().Name} is already bound!");
            }

            this.gameSystem = system;
            this.OnBindGame(system);
            this.SynchonizeWithGame(system);
        }
        
        protected virtual void OnBindGame(IGameSystem system)
        {
        }

        private void OnInitialize()
        {
            if (this.Initialize(this.gameSystem))
            {
                this.gameSystem.OnGameReady += this.OnReadyGame;
                this.gameSystem.OnGameStart += this.OnStartGame;
                this.gameSystem.OnGamePause += this.OnPauseGame;
                this.gameSystem.OnGameResume += this.OnResumeGame;
                this.gameSystem.OnGameFinish += this.OnFinishGame;
            }
        }

        protected virtual bool Initialize(IGameSystem system)
        {
            return true;
        }

        protected virtual void OnReadyGame()
        {
        }

        protected virtual void OnStartGame()
        {
        }

        protected virtual void OnPauseGame()
        {
        }

        protected virtual void OnResumeGame()
        {
        }

        protected virtual void OnFinishGame()
        {
        }

        void IGameElement.UnbindGame()
        {
            if (this.gameSystem == null)
            {
                return;
            }
            
            var system = this.gameSystem;
            this.gameSystem = null;
            
            system.OnGameInitialize -= this.OnInitialize;
            system.OnGameReady -= this.OnReadyGame;
            system.OnGameStart -= this.OnStartGame;
            system.OnGamePause -= this.OnPauseGame;
            system.OnGameResume -= this.OnResumeGame;
            system.OnGameFinish -= this.OnFinishGame;
            this.OnUnbindGame();
        }

        protected virtual void OnUnbindGame()
        {
        }

        #endregion
        
        private void SynchonizeWithGame(IGameSystem system)
        {
            var gameState = system.State;
            if (gameState >= GameState.FINISH)
            {
                return;
            }

            if (gameState < GameState.PREPARE)
            {
                system.OnGameInitialize += this.OnInitialize;
                return;
            }

            if (!this.Initialize(system))
            {
                return;
            }

            if (gameState < GameState.READY)
            {
                system.OnGameReady += this.OnReadyGame;
                return;
            }
            
            this.OnReadyGame();
            if (gameState < GameState.PLAY)
            {
                system.OnGameStart += this.OnStartGame;
                return;
            }

            this.OnStartGame();
            system.OnGamePause += this.OnPauseGame;
            system.OnGameResume += this.OnResumeGame;
            system.OnGameFinish += this.OnFinishGame;
            
            if (gameState == GameState.PAUSE)
            {
                this.OnPauseGame();
            }
        }
    }
}