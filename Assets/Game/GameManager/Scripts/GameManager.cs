using System;
using UnityEngine;

namespace Prototype.GameManagment
{
    public abstract class GameManager : MonoBehaviour
    {
        public abstract event Action OnGameLoaded;

        public abstract event Action OnGameStarted;

        public abstract event Action OnGameUnloaded;
        
        public abstract void LoadGame();

        public abstract void StartGame();

        public abstract void PauseGame();

        public abstract void DestroyGame();

        public abstract void RegisterService(object service);

        public abstract void UnregisterService(object service);

        public abstract T GetService<T>();

        public abstract bool TryGetService<T>(out T service);

        public abstract void AddGameComponent(object component);

        public abstract void RemoveGameComponent(object component);
    }
}