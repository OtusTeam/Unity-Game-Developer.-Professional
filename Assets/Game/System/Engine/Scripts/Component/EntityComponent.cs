using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class EntityComponent : MonoBehaviour, IEntityComponent
    {
        public IEntity Entity { protected get; set; }

        protected Lazy<T> GetEntityComponentLazy<T>()
        {
            return new Lazy<T>(() => this.Entity.GetEntityComponent<T>());
        }
    }
}