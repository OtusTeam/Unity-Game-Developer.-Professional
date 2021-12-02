using System;
using JetBrains.Annotations;

namespace GameElements
{
    public abstract class GameController : GameElement
    {
        [CanBeNull]
        protected IGameSystem GameSystem { get; private set; }

        #region Lifecycle

        protected sealed override void OnSetup(IGameSystem system)
        {
            if (this.GameSystem != null)
            {
                throw new Exception($"{this.GetType().Name} is already setuped!");
            }

            this.GameSystem = system;

            system.OnGamePrepare += this.OnPrepareGame;
            system.OnGameReady += this.OnReadyGame;
            system.OnGameStart += this.OnStartGame;
            system.OnGamePause += this.OnPauseGame;
            system.OnGameResume += this.OnResumeGame;
            system.OnGameFinish += this.OnFinishGame;
            this.OnSetuped(system);

            var gameState = system.State;
            if (gameState >= GameState.FINISH)
            {
                return;
            }

            if (gameState < GameState.PREPARE)
            {
                return;
            }

            this.OnPrepareGame(this);

            if (gameState >= GameState.READY)
            {
                this.OnReadyGame(this);
            }

            if (gameState >= GameState.PLAY)
            {
                this.OnStartGame(this);
            }

            if (gameState == GameState.PAUSE)
            {
                this.OnPauseGame(this);
            }
        }

        protected virtual void OnSetuped(IGameSystem system)
        {
        }

        protected virtual void OnPrepareGame(object sender)
        {
        }

        protected virtual void OnReadyGame(object sender)
        {
        }

        protected virtual void OnStartGame(object sender)
        {
        }

        protected virtual void OnPauseGame(object sender)
        {
        }

        protected virtual void OnResumeGame(object sender)
        {
        }

        protected virtual void OnFinishGame(object sender)
        {
        }

        protected sealed override void OnDispose()
        {
            base.OnDispose();

            if (this.GameSystem == null)
            {
                return;
            }

            var system = this.GameSystem;
            system.OnGamePrepare -= this.OnPrepareGame;
            system.OnGameReady -= this.OnReadyGame;
            system.OnGameStart -= this.OnStartGame;
            system.OnGamePause -= this.OnPauseGame;
            system.OnGameResume -= this.OnResumeGame;
            system.OnGameFinish -= this.OnFinishGame;

            this.OnDisposed();
        }

        protected virtual void OnDisposed()
        {
        }

        #endregion
    }
}