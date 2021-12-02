using System;
using System.Collections.Generic;
using GameElements;

namespace GameEngine
{
    public sealed class Entity : IEntity
    {
        private IGameSystem targetContext;
        
        private readonly HashSet<TagType> tags;

        private readonly Dictionary<Type, object> componentMap;

        public Entity()
        {
            this.tags = new HashSet<TagType>();
            this.componentMap = new Dictionary<Type, object>();
        }

        public void AddComponent<T>(T component)
        {
            this.componentMap.Add(typeof(T), component);
        }

        public void RemoveComponent<T>()
        {
            this.componentMap.Remove(typeof(T));
        }

        public T GetComponent<T>()
        {
            return (T) this.componentMap[typeof(T)];
        }

        public bool TryGetComponent<T>(out T component)
        {
            if (this.componentMap.TryGetValue(typeof(T), out var value))
            {
                component = (T) value;
                return true;
            }

            component = default;
            return false;
        }

        public IEnumerable<T> GetComponents<T>()
        {
            foreach (var pair in this.componentMap)
            {
                if (pair.Value is T component)
                {
                    yield return component;
                }
            }
        }

        public void AddTag(TagType tag)
        {
            this.tags.Add(tag);
        }

        public void RemoveTag(TagType tag)
        {
            this.tags.Remove(tag);
        }

        public bool ContainsTag(TagType tag)
        {
            return this.tags.Contains(tag);
        }

        public IGameSystem GetContext()
        {
            return this.targetContext;
        }

        public void BindContext(IGameSystem gameSystem)
        {
            this.targetContext = gameSystem;
        }

        public void ResetContext()
        {
            this.targetContext = null;
        }
    }
}
