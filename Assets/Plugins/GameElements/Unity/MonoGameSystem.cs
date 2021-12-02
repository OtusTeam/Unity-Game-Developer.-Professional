using System;
using UnityEngine;

namespace GameElements.Unity
{
    /// <inheritdoc cref="IGameElementSystem"/>
    public class MonoGameSystem : MonoBehaviour, IGameSystem
    {
        #region Events

        public event Action<object> OnGamePrepare
        {
            add { this.gameSystem.OnGamePrepare += value; }
            remove { this.gameSystem.OnGamePrepare -= value; }
        }

        public event Action<object> OnGameReady
        {
            add { this.gameSystem.OnGameReady += value; }
            remove { this.gameSystem.OnGameReady -= value; }
        }

        public event Action<object> OnGameStart
        {
            add { this.gameSystem.OnGameStart += value; }
            remove { this.gameSystem.OnGameStart -= value; }
        }

        public event Action<object> OnGamePause
        {
            add { this.gameSystem.OnGamePause += value; }
            remove { this.gameSystem.OnGameResume -= value; }
        }

        public event Action<object> OnGameResume
        {
            add { this.gameSystem.OnGameResume += value; }
            remove { this.gameSystem.OnGameResume -= value; }
        }

        public event Action<object> OnGameFinish
        {
            add { this.gameSystem.OnGameFinish += value; }
            remove { this.gameSystem.OnGameFinish -= value; }
        }

        #endregion

        public GameState State
        {
            get { return this.gameSystem.State; }
        }

        private readonly IGameSystem gameSystem;
        
        public MonoGameSystem()
        {
            this.gameSystem = new GameSystem();
        }

        #region Lifecycle

        public void PrepareGame(object sender)
        {
            this.gameSystem.PrepareGame(sender);
        }

        public void ReadyGame(object sender)
        {
            this.gameSystem.ReadyGame(sender);
        }

        public void StartGame(object sender)
        {
            this.gameSystem.StartGame(sender);
        }

        public void PauseGame(object sender)
        {
            this.gameSystem.PauseGame(sender);
        }

        public void ResumeGame(object sender)
        {
            this.gameSystem.ResumeGame(sender);
        }

        public void FinishGame(object sender)
        {
            this.gameSystem.FinishGame(sender);
        }

        public void DestroyGame(object sender)
        {
            this.gameSystem.DestroyGame(sender);
        }

        #endregion

        public bool AddElement(object element)
        {
            return this.gameSystem.AddElement(element);
        }

        public bool RemoveElement(object element)
        {
            return this.gameSystem.RemoveElement(element);
        }

        public T GetElement<T>()
        {
            return this.gameSystem.GetElement<T>();
        }

        public bool TryGetElement<T>(out T element)
        {
            return this.gameSystem.TryGetElement(out element);
        }
    }
}