using System;
using UnityEngine;

namespace GameElements.Unity
{
    //Дублирует код Game Controller
    public abstract class MonoGameController : MonoBehaviour, IGameElement
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

        private void OnInitialized()
        {
            if (this.Initialize(this.gameSystem))
            {
                this.gameSystem.OnGameReady += this.OnReadyGame;
                this.gameSystem.OnGameStarted += this.OnStartedGame;
                this.gameSystem.OnGamePaused += this.OnPausedGame;
                this.gameSystem.OnGameResumed += this.OnResumedGame;
                this.gameSystem.OnGameFinished += this.OnFinishedGame;
            }
        }

        protected virtual bool Initialize(IGameSystem system)
        {
            return true;
        }

        protected virtual void OnReadyGame()
        {
        }

        protected virtual void OnStartedGame()
        {
        }

        protected virtual void OnPausedGame()
        {
        }

        protected virtual void OnResumedGame()
        {
        }

        protected virtual void OnFinishedGame()
        {
        }

        void IGameElement.Dispose()
        {
            if (this.gameSystem == null)
            {
                return;
            }
            
            var system = this.gameSystem;
            this.gameSystem = null;
            
            system.OnGameInitialized -= this.OnInitialized;
            system.OnGameReady -= this.OnReadyGame;
            system.OnGameStarted -= this.OnStartedGame;
            system.OnGamePaused -= this.OnPausedGame;
            system.OnGameResumed -= this.OnResumedGame;
            system.OnGameFinished -= this.OnFinishedGame;
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

            if (gameState < GameState.INITIALIZE)
            {
                system.OnGameInitialized += this.OnInitialized;
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
                system.OnGameStarted += this.OnStartedGame;
                return;
            }

            this.OnStartedGame();
            system.OnGamePaused += this.OnPausedGame;
            system.OnGameResumed += this.OnResumedGame;
            system.OnGameFinished += this.OnFinishedGame;
            
            if (gameState == GameState.PAUSE)
            {
                this.OnPausedGame();
            }
        }
    }
}