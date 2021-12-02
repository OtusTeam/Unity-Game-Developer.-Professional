using System.Collections.Generic;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementMap{K,V}"/>
    public sealed class GameElementMap<K, V> : GameElement, IGameElementMap<K, V>
    {
        private readonly Dictionary<K, V> elementMap;

        public GameElementMap()
        {
            this.elementMap = new Dictionary<K, V>();
        }

        public GameElementMap(Dictionary<K, V> elementMap)
        {
            this.elementMap = new Dictionary<K, V>(elementMap);
        }

        public bool AddElement(K key, V element)
        {
            if (this.elementMap.ContainsKey(key))
            {
                return false;
            }

            this.elementMap.Add(key, element);
            if (element is IGameElement gameElement)
            {
                gameElement.Setup(this.GameSystem);
            }

            return true;
        }

        public bool RemoveElement(K key)
        {
            if (!this.elementMap.ContainsKey(key))
            {
                return false;
            }

            var element = this.elementMap[key];
            if (element is IGameElement gameElement)
            {
                gameElement.Dispose();
            }

            this.elementMap.Remove(key);
            return true;
        }

        public T GetElement<T>(K key)
        {
            object registeredElement = this.elementMap[key];
            return (T) registeredElement;
        }

        public bool TryGetElement<T>(K key, out T element)
        {
            if (this.elementMap.TryGetValue(key, out var result))
            {
                element = (T) (object) result;
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
            foreach (var element in this.elementMap.Values)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Dispose();
                }
            }
        }
    }
}