using System;
using UnityEngine;

namespace GameElements.Unity
{
    public class MonoGameSystem : MonoBehaviour, IGameSystem
    {
        #region Events

        public event Action OnGameInitialize
        {
            add { this.gameSystem.OnGameInitialize += value; }
            remove { this.gameSystem.OnGameInitialize -= value; }
        }

        public event Action OnGameReady
        {
            add { this.gameSystem.OnGameReady += value; }
            remove { this.gameSystem.OnGameReady -= value; }
        }

        public event Action OnGameStart
        {
            add { this.gameSystem.OnGameStart += value; }
            remove { this.gameSystem.OnGameStart -= value; }
        }

        public event Action OnGamePause
        {
            add { this.gameSystem.OnGamePause += value; }
            remove { this.gameSystem.OnGameResume -= value; }
        }

        public event Action OnGameResume
        {
            add { this.gameSystem.OnGameResume += value; }
            remove { this.gameSystem.OnGameResume -= value; }
        }

        public event Action OnGameFinish
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

        public virtual void InitializeGame()
        {
            this.gameSystem.InitializeGame();
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

        public void DestroyGame()
        {
            this.gameSystem.DestroyGame();
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