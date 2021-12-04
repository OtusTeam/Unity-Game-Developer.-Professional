using System;
using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class Entity : MonoBehaviour, IEntity, IGameElement
    {
        private GameElementLayer componentLayer;
        
        [SerializeField]
        private Parameters parameters;

        void IEntity.AddComponent(object component)
        {
            if (!this.componentLayer.AddElement(component))
            {
                return;
            }
            
            if (component is IEntityComponent entityComponent)
            {
                entityComponent.BindEntity(this);
            }
        }

        void IEntity.RemoveComponent(object component)
        {
            if (!this.componentLayer.RemoveElement(component))
            {
                return;
            }
            
            if (component is IEntityComponent entityComponent)
            {
                entityComponent.UnbindEntity();
            }
        }

        T IEntity.GetComponent<T>()
        {
            return this.componentLayer.GetElement<T>();
        }

        bool IEntity.TryGetComponent<T>(out T component)
        {
            return this.componentLayer.TryGetElement(out component);
        }

        IEnumerable<T> IEntity.GetComponents<T>()
        {
            return this.componentLayer.GetElements<T>();
        }

        void IGameElement.BindGame(IGameSystem gameSystem)
        {
            this.componentLayer.BindGame(gameSystem);
        }

        void IGameElement.Dispose()
        {
            this.componentLayer.Dispose();
        }

        private void Awake()
        {
            this.componentLayer = new GameElementLayer();
            this.InitializeComponentLayer();
        }

        private void InitializeComponentLayer()
        {
            IEntity entity = this;
            var components = this.parameters.initialComponents;
            for (int i = 0, count = components.Length; i < count; i++)
            {
                var component = components[i];
                if (component != null)
                {
                    entity.AddComponent(component);
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