using System;
using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.GameEngine
{
    [Serializable]
    public sealed class Entity : IEntity
    {
        public IGameSystem CurrentGameSystem { get; private set; }

        [ShowInInspector]
        private EntityFlag entityFlags;

        [ShowInInspector]
        private List<object> components;

        public Entity()
        {
            this.entityFlags = EntityFlag.NONE;
            this.components = new List<object>();
        }

        public void AddComponent(object component)
        {
            this.components.Add(component);
        }

        public void RemoveComponent(object component)
        {
            this.components.Remove(component);
        }

        public T GetComponent<T>()
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                if (this.components[i] is T component)
                {
                    return component;
                }
            }

            throw new Exception($"Component of type {typeof(T).Name} is not found");
        }

        public bool TryGetComponent<T>(out T component)
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                if (this.components[i] is T result)
                {
                    component = result;
                    return true;
                }
            }
            
            component = default;
            return false;
        }

        public IEnumerable<T> GetComponents<T>()
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                if (this.components[i] is T component)
                {
                    yield return component;
                }
            }
        }

        public void AddFlag(EntityFlag entityFlag)
        {
            this.entityFlags |= entityFlag;
        }

        public void RemoveFlag(EntityFlag entityFlag)
        {
            this.entityFlags &= ~entityFlag;
        }

        public bool ContainsFlag(EntityFlag entityFlag)
        {
            return this.entityFlags.HasFlag(entityFlag);
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