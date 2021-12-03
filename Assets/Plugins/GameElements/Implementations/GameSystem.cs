using System;

namespace GameElements
{
    public class GameSystem : IGameSystem
    {
        #region Event

        public event Action OnGameInitialize;

        public event Action OnGameReady;

        public event Action OnGameStart;

        public event Action OnGamePause;

        public event Action OnGameResume;

        public event Action OnGameFinish;

        #endregion
        
        public GameState State { get; protected set; }

        private readonly GameElementLayer serviceLayer;
        
        #region GameCycle

        public GameSystem()
        {
            this.State = GameState.CREATE;
            this.serviceLayer = new GameElementLayer();
        }

        public void InitializeGame()
        {
            if (this.State != GameState.CREATE)
            {
                return;
            }

            this.serviceLayer.BindGame(this);
            this.State = GameState.PREPARE;
            this.OnInitializeGame();
            this.OnGameInitialize?.Invoke();
        }

        protected virtual void OnInitializeGame()
        {
        }

        public void ReadyGame()
        {
            if (this.State != GameState.PREPARE)
            {
                return;
            }

            this.State = GameState.READY;
            this.OnReadyGame();
            this.OnGameReady?.Invoke();
        }

        protected virtual void OnReadyGame()
        {
        }

        public void StartGame()
        {
            if (this.State != GameState.READY)
            {
                return;
            }

            this.State = GameState.PLAY;
            this.OnStartGame();
            this.OnGameStart?.Invoke();
        }

        protected virtual void OnStartGame()
        {
        }

        public void PauseGame()
        {
            if (this.State == GameState.PAUSE)
            {
                return;
            }

            this.State = GameState.PAUSE;
            this.OnPauseGame();
            this.OnGamePause?.Invoke();
        }

        protected virtual void OnPauseGame()
        {
        }

        public void ResumeGame()
        {
            if (this.State != GameState.PAUSE)
            {
                return;
            }

            this.State = GameState.PLAY;
            this.OnResumeGame();
            this.OnGameResume?.Invoke();
        }

        protected virtual void OnResumeGame()
        {
        }

        public void FinishGame()
        {
            if (this.State > GameState.FINISH)
            {
                return;
            }

            this.State = GameState.FINISH;
            this.OnFinishGame();
            this.OnGameFinish?.Invoke();
        }

        protected virtual void OnFinishGame()
        {
        }

        public void DestroyGame()
        {
            if (this.State != GameState.FINISH)
            {
                return;
            }

            this.OnDestroyGame();
            this.State = GameState.DESTROY;
            IGameElement gameElement = this.serviceLayer;
            gameElement.UnbindGame();
        }

        protected virtual void OnDestroyGame()
        {
        }

        #endregion

        #region Services

        public bool AddService(object service)
        {
            return this.serviceLayer.AddElement(service);
        }

        public bool RemoveService(object service)
        {
            return this.serviceLayer.RemoveElement(service);
        }

        public T GetService<T>()
        {
            return this.serviceLayer.GetElement<T>();
        }

        public bool TryGetService<T>(out T service)
        {
            return this.serviceLayer.TryGetElement(out service);
        }

        #endregion
    }
}