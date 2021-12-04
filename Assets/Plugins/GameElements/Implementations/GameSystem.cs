using System;
using System.Collections.Generic;

namespace GameElements
{
    public class GameSystem : IGameSystem
    {
        #region Event

        public event Action OnGameInitialized;

        public event Action OnGameReady;

        public event Action OnGameStarted;

        public event Action OnGamePaused;

        public event Action OnGameResumed;

        public event Action OnGameFinished;

        #endregion
        
        public GameState State { get; protected set; }

        private readonly Layer serviceLayer;

        private readonly HashSet<IGameElement> gameElements;

        #region GameCycle

        public GameSystem()
        {
            this.State = GameState.NOT_INIITIALIZED;
            this.serviceLayer = new Layer();
            this.gameElements = new HashSet<IGameElement>();
        }

        public void InitGame()
        {
            if (this.State != GameState.NOT_INIITIALIZED)
            {
                return;
            }

            this.State = GameState.INITIALIZE;
            this.OnInitializeGame();
            this.OnGameInitialized?.Invoke();
        }

        protected virtual void OnInitializeGame()
        {
        }

        public void ReadyGame()
        {
            if (this.State != GameState.INITIALIZE)
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

            this.State = GameState.PAUSE;
            this.OnPauseGame();
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

            this.State = GameState.PLAY;
            this.OnResumeGame();
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

            this.State = GameState.FINISH;
            this.OnFinishGame();
            this.OnGameFinished?.Invoke();
        }

        protected virtual void OnFinishGame()
        {
        }

        public void DisposeGame()
        {
            if (this.State != GameState.FINISH)
            {
                return;
            }
            
            this.OnDestroyGame();
            this.State = GameState.NOT_INIITIALIZED;
        }

        public void AddElement(IGameElement element)
        {
            var gameElements = GameElementUtils.CollectElements(element);
            foreach (var gameElement in gameElements)
            {
                
                
                if (this.)
                {
                    
                }
                
                if (gameElement is IGameElement)
                {
                    
                }
            }
        }

        public void RemoveElement(IGameElement element)
        {
            
        }

        protected virtual void OnDestroyGame()
        {
        }

        #endregion

        #region Services

        public bool RegisterService(object service)
        {
            return this.serviceLayer.Add(service);
        }

        public bool UnregisterService(object service)
        {
            return this.serviceLayer.Remove(service);
        }

        public T GetService<T>()
        {
            return this.serviceLayer.Get<T>();
        }

        public bool TryGetService<T>(out T service)
        {
            return this.serviceLayer.TryGet(out service);
        }

        #endregion
        
        
        

        private void ActivateElement(IGameElement element, IGameSystem system)
        {
            if (!this.gameElements.Add(element))
            {
                return;
            }
            
            
            
        }
        
        private void DeactivateElement(IGameElement element, IGameSystem system)
        {
            if (this.gameElements.Remove(element))
            {
                if (element is IGameDisposeElement disposeElement)
                {
                    disposeElement.DisposeGame(system);
                }
            }
        }
    }
}