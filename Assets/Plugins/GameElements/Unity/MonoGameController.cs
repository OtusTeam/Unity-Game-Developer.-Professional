using System;

namespace GameElements.Unity
{
    public abstract class MonoGameController : MonoGameElement
    {
        protected IGameSystem CurrentGameSystem { get; private set; }

        #region Lifecycle

        protected sealed override void OnSetup(IGameSystem system)
        {
            if (this.CurrentGameSystem != null)
            {
                throw new Exception($"{this.GetType().Name} is already setuped!");
            }

            this.CurrentGameSystem = system;

            system.OnGamePrepare += this.OnPrepareGame;
            system.OnGameReady += this.OnReadyGame;
            system.OnGameStart += this.OnStartGame;
            system.OnGamePause += this.OnPauseGame;
            system.OnGameResume += this.OnResumeGame;
            system.OnGameFinish += this.OnFinishGame;

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

            if (this.CurrentGameSystem == null)
            {
                return;
            }

            var system = this.CurrentGameSystem;
            system.OnGamePrepare -= this.OnPrepareGame;
            system.OnGameReady -= this.OnReadyGame;
            system.OnGameStart -= this.OnStartGame;
            system.OnGamePause -= this.OnPauseGame;
            system.OnGameResume -= this.OnResumeGame;
            system.OnGameFinish -= this.OnFinishGame;
        }

        #endregion
    }
}