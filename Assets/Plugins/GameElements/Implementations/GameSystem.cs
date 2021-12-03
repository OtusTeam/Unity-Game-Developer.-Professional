using System;
using System.Collections.Generic;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementSystem"/>
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

        private readonly Dictionary<Type, object> elementMap;

        #region Lifecycle

        public GameSystem()
        {
            this.State = GameState.CREATE;
            this.elementMap = new Dictionary<Type, object>();
        }

        public void InitializeGame()
        {
            if (this.State != GameState.CREATE)
            {
                return;
            }

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

            this.State = GameState.DESTROY;
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }
            }
            
            this.OnDestroyGame();
        }

        protected virtual void OnDestroyGame()
        {
        }

        #endregion

        public bool AddElement(object element)
        {
            var type = element.GetType();
            if (this.elementMap.ContainsKey(type))
            {
                return false;
            }

            this.elementMap.Add(type, element);
            if (element is IGameElement controller)
            {
                controller.BindGame(this);
            }

            return true;
        }

        public bool RemoveElement(object element)
        {
            var type = element.GetType();
            if (!this.elementMap.Remove(type))
            {
                return false;
            }

            if (element is IGameElement gameElement)
            {
                gameElement.UnbindGame();
            }

            return true;
        }

        public T GetElement<T>()
        {
            return GameElementUtils.FindValue<T>(this.elementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            if (GameElementUtils.TryFindValue(this.elementMap, out T result))
            {
                element = result;
                return true;
            }

            element = default;
            return false;
        }
    }
}