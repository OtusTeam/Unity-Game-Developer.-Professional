using UnityEngine;

namespace GameElements.Unity
{
    public abstract class MonoGameElementController : MonoBehaviour, IGameInitElement
    {
        void IGameInitElement.InitGame(IGameSystem gameSystem)
        {
            if (!this.Initialize(gameSystem))
            {
                gameSystem.RemoveElement(this);
            }
        }

        protected abstract bool Initialize(IGameSystem gameSystem);
    }
}