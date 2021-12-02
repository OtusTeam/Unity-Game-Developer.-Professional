using System;
using System.Collections.Generic;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementLayer"/>
    public sealed class GameElementLayer : GameElement, IGameElementLayer
    {
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
            if (element is IGameElement gameElement)
            {
                gameElement.Setup(this.GameSystem);
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
            return GameElementUtils.Find<T>(this.elementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            if (GameElementUtils.TryFind(this.elementMap, out IGameElement result))
            {
                element = (T) result;
                return true;
            }

            element = default;
            return false;
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Setup(this.GameSystem);
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