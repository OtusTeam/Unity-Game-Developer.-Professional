using System;
using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MonoEntity : SerializedMonoBehaviour, IEntity
    {
        public IGameSystem CurrentGameSystem
        {
            get { return this.entity.CurrentGameSystem; }
        }

        [ReadOnly]
        [ShowInInspector]
        private Entity entity;

        [SerializeField]
        private Parameters parameters;

        void IEntity.AddComponent(object component)
        {
            this.entity.AddComponent(component);
        }

        void IEntity.RemoveComponent(object component)
        {
            this.entity.RemoveComponent(component);
        }

        IEnumerable<T> IEntity.GetComponents<T>()
        {
            return this.entity.GetComponents<T>();
        }

        void IEntity.AddFlag(EntityFlag entityFlag)
        {
            this.entity.AddFlag(entityFlag);
        }

        void IEntity.RemoveFlag(EntityFlag entityFlag)
        {
            this.entity.RemoveFlag(entityFlag);
        }

        bool IEntity.ContainsFlag(EntityFlag entityFlag)
        {
            return this.entity.ContainsFlag(entityFlag);
        }

        void IEntity.BindContext(IGameSystem gameSystem)
        {
            this.entity.BindContext(gameSystem);
        }

        void IEntity.ResetContext()
        {
            this.entity.ResetContext();
        }

        private void Awake()
        {
            this.InitializeEntity();
        }

        private void InitializeEntity()
        {
            this.entity = new Entity();
            this.entity.AddFlag(this.parameters.initialFlags);

            var components = this.parameters.initialComponents;
            for (int i = 0, count = components.Length; i < count; i++)
            {
                var component = components[i];
                if (component != null)
                {
                    this.entity.AddComponent(component);
                }
            }
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public EntityFlag initialFlags;
            
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