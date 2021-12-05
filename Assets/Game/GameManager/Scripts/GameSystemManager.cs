using System;
using System.Collections;
using System.Collections.Generic;
using GameElements;
using GameElements.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Prototype.GameManagment
{
    //TODO: Абстрактный пример GameManager... 
    public sealed class GameSystemManager : GameManager
    {
        public override event Action OnGameLoaded;

        public override event Action OnGameStarted;
        
        public override event Action OnGameUnloaded;

        private const string GAME_SCENE = "GameScene";
        
        private const string GAME_CONTEXT_TAG = "GameContext";

        private MonoGameSystem gameSystem;

        [SerializeField]
        private Parameters parameters;

        private readonly HashSet<object> servicesCache;

        private readonly HashSet<IGameElement> gameElementsCache;

        public GameSystemManager()
        {
            this.servicesCache = new HashSet<object>();
            this.gameElementsCache = new HashSet<IGameElement>();
        }

        public override void LoadGame()
        {
            this.StartCoroutine(this.LoadGameRoutine());
        }

        public override void StartGame()
        {
            this.gameSystem.StartGame();
        }

        public override void PauseGame()
        {
            this.gameSystem.PauseGame();
        }

        public override void DestroyGame()
        {
            this.StartCoroutine(this.UnloadGameRoutine());
        }

        public override void RegisterService(object service)
        {
            if (!this.servicesCache.Add(service))
            {
                return;
            }
            
            if (this.gameSystem != null)
            {
                this.gameSystem.RegisterService(service);
            }
        }

        public override void UnregisterService(object service)
        {
            if (!this.servicesCache.Remove(service))
            {
                return;
            }
            
            if (this.gameSystem != null)
            {
                this.gameSystem.UnregisterService(service);
            }
        }
        
        public override T GetService<T>()
        {
            return this.gameSystem.GetService<T>();
        }

        public override bool TryGetService<T>(out T service)
        {
            return this.gameSystem.TryGetService(out service);
        }
        
        public override void AddGameComponent(object target)
        {
            if (!(target is IGameElement gameElement))
            {
                return;
            }

            if (!this.gameElementsCache.Add(gameElement))
            {
                return;
            }

            if (this.gameSystem != null)
            {
                this.gameSystem.AddElement(gameElement);
            }
        }

        public override void RemoveGameComponent(object component)
        {
            if (!(component is IGameElement gameElement))
            {
                return;
            }

            if (!this.gameElementsCache.Remove(gameElement))
            {
                return;
            }

            if (this.gameSystem != null)
            {
                this.gameSystem.RemoveElement(gameElement);
            }
        }

        private IEnumerator LoadGameRoutine()
        {
            var operation = SceneManager.LoadSceneAsync(GAME_SCENE, LoadSceneMode.Single);
            while (!operation.isDone)
            {
                yield return null;
            }

            var gameSystemGO = GameObject.FindWithTag(GAME_CONTEXT_TAG);
            var gameSystem = gameSystemGO.GetComponent<MonoGameSystem>();

            foreach (var externalService in this.servicesCache)
            {
                gameSystem.RegisterService(externalService);
            }

            foreach (var externalElement in this.gameElementsCache)
            {
                gameSystem.AddElement(externalElement);
            }

            yield return new WaitForEndOfFrame();
            gameSystem.InitGame();
            gameSystem.ReadyGame();
            
            this.gameSystem = gameSystem;
            this.OnGameLoaded?.Invoke();
        }

        private IEnumerator UnloadGameRoutine()
        {
            var gameSystem = this.gameSystem;
            this.gameSystem = null;

            foreach (var externalElement in this.gameElementsCache)
            {
                gameSystem.RemoveElement(externalElement);
            }

            foreach (var externalService in this.servicesCache)
            {
                gameSystem.UnregisterService(externalService);
            }

            var operation = SceneManager.UnloadSceneAsync(GAME_SCENE);
            while (!operation.isDone)
            {
                yield return null;
            }
            
            this.OnGameUnloaded?.Invoke();
        }
        
        private void Awake()
        {
            foreach (var service in this.parameters.services)
            {
                this.servicesCache.Add(service);
            }

            foreach (var element in this.parameters.gameElements)
            {
                if (element is IGameElement gameElement)
                {
                    this.gameElementsCache.Add(gameElement);
                }
            }
        }

        [Serializable]
        public sealed class Parameters
        {
            [Space]
            [SerializeField]
            public Object[] services;

            [Space]
            [SerializeField]
            public Object[] gameElements;
        }
    }
}