using System;
using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    public sealed class GameElementLayer : IGameElement, IEnumerable<object>
    {
        private IGameSystem gameSystem;
        
        private readonly Dictionary<Type, object> elementMap;

        public GameElementLayer()
        {
            this.elementMap = new Dictionary<Type, object>();
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
            var requiredType = typeof(T);
            if (this.elementMap.ContainsKey(requiredType))
            {
                return (T) this.elementMap[requiredType];
            }

            foreach (var key in this.elementMap.Keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return (T) this.elementMap[key];
                }
            }

            throw new Exception($"Element of type {requiredType.Name} is not found!");
        }
        
        public IEnumerable<T> GetElements<T>()
        {
            foreach (var pair in this.elementMap)
            {
                var element = pair.Value;
                if (element is T tElement)
                {
                    yield return tElement;
                }
            }
        }

        public bool TryGetElement<T>(out T element)
        {
            var requiredType = typeof(T);
            if (this.elementMap.ContainsKey(requiredType))
            {
                element = (T) this.elementMap[requiredType];
                return true;
            }

            foreach (var key in this.elementMap.Keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    element = (T) this.elementMap[key];
                    return true;
                }
            }

            element = default;
            return false;
        }

        public void BindGame(IGameSystem system)
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

        public void UnbindGame()
        {
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach (var element in this.elementMap.Values)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}