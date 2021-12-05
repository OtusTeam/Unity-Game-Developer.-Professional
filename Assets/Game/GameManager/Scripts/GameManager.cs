using UnityEngine;

namespace Prototype.GameManagment
{
    public abstract class GameManager : MonoBehaviour
    {
        public abstract void LoadGame();

        public abstract void StartGame();

        public abstract void PauseGame();

        public abstract void DestroyGame();

        public abstract void RegisterService(object service);

        public abstract void UnregisterService(object service);

        public abstract T GetService<T>();

        public abstract bool TryGetService<T>(out T service);

        public abstract void InjectGame(object target);
    }
}