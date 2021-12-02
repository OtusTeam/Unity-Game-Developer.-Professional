using System;
using System.Collections.Generic;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementSystem"/>
    public class GameSystem : IGameSystem
    {
        #region Event

        public event Action<object> OnGamePrepare;

        public event Action<object> OnGameReady;

        public event Action<object> OnGameStart;

        public event Action<object> OnGamePause;

        public event Action<object> OnGameResume;

        public event Action<object> OnGameFinish;

        #endregion
        
        public GameState State { get; protected set; }

        private readonly Dictionary<Type, object> elementMap;

        #region Lifecycle

        public GameSystem()
        {
            this.State = GameState.CREATE;
            this.elementMap = new Dictionary<Type, object>();
        }

        public void PrepareGame(object sender)
        {
            if (this.State != GameState.CREATE)
            {
                return;
            }

            this.State = GameState.PREPARE;
            this.OnPrepareGame(sender);
            this.OnGamePrepare?.Invoke(sender);
        }

        protected virtual void OnPrepareGame(object sender)
        {
        }

        public void ReadyGame(object sender)
        {
            if (this.State != GameState.PREPARE)
            {
                return;
            }

            this.State = GameState.READY;
            this.OnReadyGame(this);
            this.OnGameReady?.Invoke(sender);
        }

        protected virtual void OnReadyGame(object sender)
        {
        }

        public void StartGame(object sender)
        {
            if (this.State != GameState.READY)
            {
                return;
            }

            this.State = GameState.PLAY;
            this.OnStartGame(sender);
            this.OnGameStart?.Invoke(sender);
        }

        protected virtual void OnStartGame(object sender)
        {
        }

        public void PauseGame(object sender)
        {
            if (this.State == GameState.PAUSE)
            {
                return;
            }

            this.State = GameState.PAUSE;
            this.OnPauseGame(sender);
            this.OnGamePause?.Invoke(sender);
        }

        protected virtual void OnPauseGame(object sender)
        {
        }

        public void ResumeGame(object sender)
        {
            if (this.State != GameState.PAUSE)
            {
                return;
            }

            this.State = GameState.PLAY;
            this.OnResumeGame();
            this.OnGameResume?.Invoke(sender);
        }

        protected virtual void OnResumeGame()
        {
        }

        public void FinishGame(object sender)
        {
            if (this.State > GameState.FINISH)
            {
                return;
            }

            this.State = GameState.FINISH;
            this.OnFinishGame(sender);
            this.OnGameFinish?.Invoke(sender);
        }

        protected virtual void OnFinishGame(object sender)
        {
        }

        public void DestroyGame(object sender)
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
                    gameElement.Dispose();
                }
            }
            
            this.OnDestroyGame(sender);
        }

        protected virtual void OnDestroyGame(object sender)
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
                controller.Setup(this);
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
                gameElement.Dispose();
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