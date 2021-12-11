using System;
using System.Collections;
using UnityEngine;

namespace GameElements.Unity
{
    public class MonoGameSystem : MonoBehaviour, IGameSystem
    {
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

        public GameState State
        {
            get { return this.gameSystem.State; }
        }

        private readonly IGameSystem gameSystem;

        public MonoGameSystem()
        {
            this.gameSystem = new GameSystem();
        }

        public void InitGame()
        {
            this.LoadServices();
            this.LoadGameElements();
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

        public void AddService(object service)
        {
            this.gameSystem.AddService(service);
        }

        public void RemoveService(object service)
        {
            this.gameSystem.RemoveService(service);
        }

        public T GetService<T>()
        {
            return this.gameSystem.GetService<T>();
        }

        public bool TryGetService<T>(out T service)
        {
            return this.gameSystem.TryGetService(out service);
        }

        [SerializeField]
        private bool autoRun;

        [Space]
        [SerializeField]
        private MonoBehaviour[] gameServices;

        [Space]
        [SerializeField]
        private MonoBehaviour[] gameElements;

        private IEnumerator Start()
        {
            if (this.autoRun)
            {
                yield return new WaitForEndOfFrame();
                this.InitGame();
                this.ReadyGame();
                this.StartGame();
            }
        }

        private void LoadGameElements()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var monoElement = this.gameElements[i];
                if (monoElement is IGameElement gameElement)
                {
                    this.AddElement(gameElement);
                }
            }
        }

        private void LoadServices()
        {
            for (int i = 0, count = this.gameServices.Length; i < count; i++)
            {
                var monoService = this.gameServices[i];
                if (monoService != null)
                {
                    this.AddService(monoService);
                }
            }
        }

#if UNITY_EDITOR

        protected virtual void OnValidate()
        {
            this.gameElements = MonoValidator.ValidateGameElements(this.gameElements);
            this.gameServices = MonoValidator.ValidateServices(this.gameServices);
        }
#endif
    }
}