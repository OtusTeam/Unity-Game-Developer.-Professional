using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class EntityComponent : MonoBehaviour, IEntityComponent
    {
        IEntity IEntityComponent.Entity
        {
            set { this.entity = value; }
        }

        private IEntity entity;

        protected Lazy<T> GetComponentLazy<T>()
        {
            return new Lazy<T>(() => this.entity.GetComponent<T>());
        }
    }
}