using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class MonoEntityComponent : MonoBehaviour, IEntityComponent
    {
        public IEntity Entity { get; private set; }

        void IEntityComponent.BindEntity(IEntity entity)
        {
            this.Entity = entity;
        }

        void IEntityComponent.ResetEntity()
        {
            this.Entity = null;
        }

        protected Lazy<T> GetComponentLazy<T>()
        {
            return new Lazy<T>(() => this.Entity.GetComponent<T>());
        }

        protected Lazy<T> GetSubsystemLazy<T>()
        {
            return new Lazy<T>(() => this.Entity.GameSystem.GetElement<T>());
        }
    }
}