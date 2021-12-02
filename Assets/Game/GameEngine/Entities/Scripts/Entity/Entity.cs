using System;
using System.Collections.Generic;
using GameElements;

namespace GameEngine
{
    public sealed class Entity : IEntity
    {
        public IGameSystem CurrentGameSystem { get; private set; }

        private readonly Dictionary<Type, object> componentMap;

        private FlagType flags;

        public Entity()
        {
            this.flags = FlagType.NONE;
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

        public void AddFlag(FlagType flag)
        {
            this.flags |= flag;
        }

        public void RemoveFlag(FlagType flag)
        {
            this.flags &= ~flag;
        }

        public bool ContainsFlag(FlagType flag)
        {
            return this.flags.HasFlag(flag);
        }

        public void BindContext(IGameSystem gameSystem)
        {
            this.CurrentGameSystem = gameSystem;
        }

        public void ResetContext()
        {
            this.CurrentGameSystem = null;
        }
    }
}