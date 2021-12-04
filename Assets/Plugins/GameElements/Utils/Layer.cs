using System;
using System.Collections;
using System.Collections.Generic;

namespace GameElements
{
    public sealed class Layer : IEnumerable<object>
    {
        private readonly Dictionary<Type, object> itemMap;

        public Layer()
        {
            this.itemMap = new Dictionary<Type, object>();
        }

        public bool Add(object item)
        {
            var type = item.GetType();
            if (this.itemMap.ContainsKey(type))
            {
                return false;
            }

            this.itemMap.Add(type, item);
            return true;
        }

        public bool Remove(object item)
        {
            var type = item.GetType();
            if (!this.itemMap.Remove(type))
            {
                return false;
            }

            return true;
        }

        public T Get<T>()
        {
            var requiredType = typeof(T);
            if (this.itemMap.TryGetValue(requiredType, out var item))
            {
                return (T) item;
            }

            foreach (var key in this.itemMap.Keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return (T) this.itemMap[key];
                }
            }

            throw new Exception($"Element of type {requiredType.Name} is not found!");
        }

        public IEnumerable<T> All<T>()
        {
            foreach (var pair in this.itemMap)
            {
                if (pair.Value is T tElement)
                {
                    yield return tElement;
                }
            }
        }

        public bool TryGet<T>(out T item)
        {
            var requiredType = typeof(T);
            if (this.itemMap.TryGetValue(requiredType, out var value))
            {
                item = (T) value;
                return true;
            }

            foreach (var key in this.itemMap.Keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    item = (T) this.itemMap[key];
                    return true;
                }
            }

            item = default;
            return false;
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach (var item in this.itemMap.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}