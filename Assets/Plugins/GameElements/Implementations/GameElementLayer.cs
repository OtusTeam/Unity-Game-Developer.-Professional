using System;
using System.Collections.Generic;

namespace GameElements
{
    public sealed class GameElementLayer : GameElement
    {
        private IGameSystem gameSystem;
        
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
            
            if (this.gameSystem != null)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.BindGame(this.gameSystem);
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

            if (this.gameSystem != null)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }    
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

        protected override void BindGame(IGameSystem system)
        {
            this.gameSystem = system;
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.BindGame(system);
                }
            }
        }

        protected override void UnbindGame()
        {
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }
            }
        }
    }
}