using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class EntityComponent : MonoBehaviour, IEntityComponent
    {
        protected IEntity Entity { get; private set; }

        void IEntityComponent.SetEntity(IEntity entity)
        {
            this.Entity = entity;
        }

        protected Lazy<T> GetComponentLazy<T>()
        {
            return new Lazy<T>(() => this.Entity.GetComponent<T>());
        }
    }
}