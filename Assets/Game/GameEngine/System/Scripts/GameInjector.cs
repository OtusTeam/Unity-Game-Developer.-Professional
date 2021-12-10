using GameElements;
using GameElements.Unity;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class GameInjector : MonoInjector
    {
        [SerializeField]
        private MonoGameSystem gameSystem;
        
        public override void InjectContextInto(object target)
        {
            if (target is IGameElement gameElement)
            {
                this.gameSystem.AddElement(gameElement);
            }
        }
    }
}