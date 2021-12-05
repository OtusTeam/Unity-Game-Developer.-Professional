using System.Collections;
using System.Collections.Generic;
using GameElements;
using GameElements.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype.GameManagment
{
    //TODO: Абстрактный пример GameManager... 
    public sealed class GameSystemManager : GameManager
    {
        private const string GAME_SCENE = "GameScene";
        
        private const string GAME_CONTEXT_TAG = "GameContext";

        private MonoGameSystem gameSystem;

        private readonly HashSet<object> serviceCache;

        private readonly HashSet<IGameElement> injectCache;

        public GameSystemManager()
        {
            this.serviceCache = new HashSet<object>();
            this.injectCache = new HashSet<IGameElement>();
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
            if (!this.serviceCache.Add(service))
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
            if (!this.serviceCache.Remove(service))
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
        
        public override void InjectGame(object target)
        {
            if (!(target is IGameElement gameElement))
            {
                return;
            }

            if (!this.injectCache.Add(gameElement))
            {
                return;
            }

            if (this.gameSystem != null)
            {
                this.gameSystem.AddElement(gameElement);
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

            foreach (var externalService in this.serviceCache)
            {
                gameSystem.RegisterService(externalService);
            }

            foreach (var externalElement in this.injectCache)
            {
                gameSystem.AddElement(externalElement);
            }

            yield return new WaitForEndOfFrame();
            gameSystem.InitGame();
            gameSystem.ReadyGame();
            
            this.gameSystem = gameSystem;
        }

        private IEnumerator UnloadGameRoutine()
        {
            var gameSystem = this.gameSystem;
            this.gameSystem = null;

            foreach (var externalElement in this.injectCache)
            {
                gameSystem.RemoveElement(externalElement);
            }

            foreach (var externalService in this.serviceCache)
            {
                gameSystem.UnregisterService(externalService);
            }

            var operation = SceneManager.UnloadSceneAsync(GAME_SCENE);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}