namespace GameElements.Unity
{
    public abstract class MonoGameController : MonoGameElement
    {
        #region Lifecycle

        protected sealed override void OnSetup()
        {
            base.OnSetup();
            
            var system = this.GameSystem;
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
            
            var system = this.GameSystem;
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