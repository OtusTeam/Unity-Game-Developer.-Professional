using System;
using System.Collections.Generic;
using GameElements;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.GameEngine
{
    [Serializable]
    public sealed class Entity : IEntity
    {
        [CanBeNull]
        public IGameSystem GameSystem { get; private set; }

        [ShowInInspector]
        private List<object> components;

        public Entity()
        {
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
        
        public void BindContext(IGameSystem gameSystem)
        {
            this.GameSystem = gameSystem;
        }

        public void ResetContext()
        {
            this.GameSystem = null;
        }
    }
}