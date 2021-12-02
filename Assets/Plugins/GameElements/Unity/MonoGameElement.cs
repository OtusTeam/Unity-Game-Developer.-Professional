using UnityEngine;

namespace GameElements.Unity
{
    public abstract class MonoGameElement : MonoBehaviour, IGameElement
    {
        void IGameElement.Setup(IGameSystem system)
        {
            this.OnSetup(system);
        }

        protected virtual void OnSetup(IGameSystem system)
        {
        }

        void IGameElement.Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}