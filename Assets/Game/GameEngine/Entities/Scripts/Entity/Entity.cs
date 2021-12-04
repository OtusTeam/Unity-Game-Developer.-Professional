using System;
using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class Entity : MonoBehaviour, IEntity, IGameInitElement
    {
        private IGameSystem gameSystem;
        
        private GenericDictionary componentMap;
        
        [SerializeField]
        private Parameters parameters;

        void IEntity.AddComponent(object component)
        {
            if (!this.componentMap.Add(component))
            {
                return;
            }
            
            if (component is IEntityComponent entityComponent)
            {
                entityComponent.SetEntity(this);
            }
        }

        void IEntity.RemoveComponent(object component)
        {
            this.componentMap.Remove(component);
        }

        T IEntity.GetComponent<T>()
        {
            return this.componentMap.Get<T>();
        }

        bool IEntity.TryGetComponent<T>(out T component)
        {
            return this.componentMap.TryGet(out component);
        }

        IEnumerable<T> IEntity.GetComponents<T>()
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
                    this.componentMap.Add(component);
                }
            }
        }

        void IGameInitElement.InitGame(IGameSystem gameSystem)
        {
            this.gameSystem = gameSystem;
            this.InitializeComponents(gameSystem);
        }

        private void InitializeComponents(IGameSystem gameSystem)
        {
            foreach (var component in this.componentMap)
            {
                if (component is IGameElement gameElement)
                {
                    gameSystem.AddElement(gameElement);
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