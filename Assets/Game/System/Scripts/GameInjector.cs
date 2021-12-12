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
            if (target is GameObject gameObject)
            {
                var gameElements = gameObject.GetComponents<IGameElement>();
                foreach (var element in gameElements)
                {
                    this.gameSystem.AddElement(element);
                }
                
                return;
            }

            if (target is MonoBehaviour monoBehaviour)
            {
                var gameElements = monoBehaviour.GetComponents<IGameElement>();
                foreach (var element in gameElements)
                {
                    this.gameSystem.AddElement(element);
                }
                
                return;
            }
            
            if (target is IGameElement gameElement)
            {
                this.gameSystem.AddElement(gameElement);
            }
        }
    }
}