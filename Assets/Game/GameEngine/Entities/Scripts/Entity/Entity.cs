using System;
using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        public int Id { get; private set; }

        private GenericDictionary componentMap;

        [SerializeField]
        private Parameters parameters;

        void IEntity.SetupId(int id)
        {
            this.Id = id;
        }

        public void AddEntityComponent(object component)
        {
            if (!this.componentMap.Add(component))
            {
                return;
            }

            if (component is IEntityComponent entityComponent)
            {
                entityComponent.Entity = this;
            }
        }

        public void RemoveEntityComponent(object component)
        {
            this.componentMap.Remove(component);
        }

        public T GetEntityComponent<T>()
        {
            return this.componentMap.Get<T>();
        }

        public bool TryGetEntityComponent<T>(out T component)
        {
            return this.componentMap.TryGet(out component);
        }

        public IEnumerable<T> GetEntityComponents<T>()
        {
            return this.componentMap.All<T>();
        }

        private void Awake()
        {
            this.SetupComponentMap();
        }

        private void SetupComponentMap()
        {
            this.componentMap = new GenericDictionary();
            var components = this.parameters.initialComponents;
            for (int i = 0, count = components.Length; i < count; i++)
            {
                var component = components[i];
                if (component != null)
                {
                    this.AddEntityComponent(component);
                }
            }
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public MonoBehaviour[] initialComponents;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.Awake();
        }
#endif
    }
}