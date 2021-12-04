using System;
using UnityEngine;

namespace GameElements.Unity
{
    public class MonoGameSystem : MonoBehaviour, IGameSystem
    {
        #region Events

        public event Action OnGameInitialized
        {
            add { this.gameSystem.OnGameInitialized += value; }
            remove { this.gameSystem.OnGameInitialized -= value; }
        }

        public event Action OnGameReady
        {
            add { this.gameSystem.OnGameReady += value; }
            remove { this.gameSystem.OnGameReady -= value; }
        }

        public event Action OnGameStarted
        {
            add { this.gameSystem.OnGameStarted += value; }
            remove { this.gameSystem.OnGameStarted -= value; }
        }

        public event Action OnGamePaused
        {
            add { this.gameSystem.OnGamePaused += value; }
            remove { this.gameSystem.OnGameResumed -= value; }
        }

        public event Action OnGameResumed
        {
            add { this.gameSystem.OnGameResumed += value; }
            remove { this.gameSystem.OnGameResumed -= value; }
        }

        public event Action OnGameFinished
        {
            add { this.gameSystem.OnGameFinished += value; }
            remove { this.gameSystem.OnGameFinished -= value; }
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

        public virtual void InitGame()
        {
            this.gameSystem.InitGame();
        }

        public void ReadyGame()
        {
            this.gameSystem.ReadyGame();
        }

        public void StartGame()
        {
            this.gameSystem.StartGame();
        }

        public void PauseGame()
        {
            this.gameSystem.PauseGame();
        }

        public void ResumeGame()
        {
            this.gameSystem.ResumeGame();
        }

        public void FinishGame()
        {
            this.gameSystem.FinishGame();
        }

        public void AddElement(IGameElement element)
        {
            this.gameSystem.AddElement(element);
        }

        public void RemoveElement(IGameElement element)
        {
            this.gameSystem.RemoveElement(element);
        }

        #endregion

        public bool RegisterService(object service)
        {
            return this.gameSystem.RegisterService(service);
        }

        public bool UnregisterService(object service)
        {
            return this.gameSystem.UnregisterService(service);
        }

        public T GetService<T>()
        {
            return this.gameSystem.GetService<T>();
        }

        public bool TryGetService<T>(out T service)
        {
            return this.gameSystem.TryGetService(out service);
        }
    }
}