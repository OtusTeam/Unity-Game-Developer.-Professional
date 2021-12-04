using System;

namespace GameElements
{
    public class GameSystem : IGameSystem
    {
        public event Action OnGameInitialized;

        public event Action OnGameReady;

        public event Action OnGameStarted;

        public event Action OnGamePaused;

        public event Action OnGameResumed;

        public event Action OnGameFinished;

        public GameState State { get; protected set; }

        private readonly GenericDictionary serviceMap;

        private readonly IGameElementContext elementContext;

        public GameSystem()
        {
            this.State = GameState.NOT_INITITALIZED;
            this.elementContext = new GameElementContext(this);
            this.serviceMap = new GenericDictionary();
        }

        public void InitGame()
        {
            if (this.State != GameState.NOT_INITITALIZED)
            {
                return;
            }

            this.OnInitGame();
            this.State = GameState.INITIALIZED;
            this.elementContext.InitGame();
            this.OnGameInitialized?.Invoke();
        }

        protected virtual void OnInitGame()
        {
        }

        public void ReadyGame()
        {
            if (this.State != GameState.INITIALIZED)
            {
                return;
            }

            this.OnReadyGame();
            this.State = GameState.READY;
            this.elementContext.ReadyGame();
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

            this.OnStartGame();
            this.State = GameState.PLAY;
            this.elementContext.StartGame();
            this.OnGameStarted?.Invoke();
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

            this.OnPauseGame();
            this.State = GameState.PAUSE;
            this.elementContext.PauseGame();
            this.OnGamePaused?.Invoke();
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

            this.OnResumeGame();
            this.State = GameState.PLAY;
            this.elementContext.ResumeGame();
            this.OnGameResumed?.Invoke();
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

            this.OnFinishGame();
            this.State = GameState.FINISH;
            this.elementContext.FinishGame();
            this.OnGameFinished?.Invoke();
        }

        protected virtual void OnFinishGame()
        {
        }

        public void AddElement(IGameElement element)
        {
            this.elementContext.AddElement(element);
        }

        public void RemoveElement(IGameElement element)
        {
            this.elementContext.RemoveElement(element);
        }

        public bool RegisterService(object service)
        {
            return this.serviceMap.Add(service);
        }

        public bool UnregisterService(object service)
        {
            return this.serviceMap.Remove(service);
        }

        public T GetService<T>()
        {
            return this.serviceMap.Get<T>();
        }

        public bool TryGetService<T>(out T service)
        {
            return this.serviceMap.TryGet(out service);
        }
    }
}