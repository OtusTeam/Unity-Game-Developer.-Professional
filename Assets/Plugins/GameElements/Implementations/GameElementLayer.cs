using System;
using System.Collections.Generic;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementLayer"/>
    public sealed class GameElementLayer : GameElement, IGameElementLayer
    {
        private IGameSystem currentGameSystem;
        
        private readonly Dictionary<Type, object> elementMap;

        public GameElementLayer()
        {
            this.elementMap = new Dictionary<Type, object>();
        }
        
        public GameElementLayer(Dictionary<Type, object> elementMap)
        {
            this.elementMap = new Dictionary<Type, object>(elementMap);
        }

        public bool AddElement(object element)
        {
            var type = element.GetType();
            if (this.elementMap.ContainsKey(type))
            {
                return false;
            }

            this.elementMap.Add(type, element);
            
            if (this.currentGameSystem != null)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Setup(this.currentGameSystem);
                }
            }
            
            return true;
        }

        public bool RemoveElement(object element)
        {
            var type = element.GetType();
            if (!this.elementMap.Remove(type))
            {
                return false;
            }

            if (element is IGameElement gameElement)
            {
                gameElement.Dispose();
            }

            return true;
        }

        public T GetElement<T>()
        {
            return GameElementUtils.FindValue<T>(this.elementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            if (GameElementUtils.TryFindValue(this.elementMap, out IGameElement result))
            {
                element = (T) result;
                return true;
            }

            element = default;
            return false;
        }

        protected override void OnSetup(IGameSystem system)
        {
            base.OnSetup(system);
            this.currentGameSystem = system;
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Setup(system);
                }
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            var elements = this.elementMap.Values;
            foreach (var element in elements)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Dispose();
                }
            }
        }
    }
}