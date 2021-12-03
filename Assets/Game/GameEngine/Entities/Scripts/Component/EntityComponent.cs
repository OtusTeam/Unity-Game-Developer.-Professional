using System;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class EntityComponent : MonoBehaviour, IEntityComponent, IGameElement
    {
        protected IEntity Entity { get; private set; }

        protected IGameSystem GameSystem { get; private set; }

        void IEntityComponent.BindEntity(IEntity entity)
        {
            this.Entity = entity;
        }

        void IEntityComponent.UnbindEntity()
        {
            this.Entity = null;
        }
        
        void IGameElement.BindGame(IGameSystem system)
        {
            this.GameSystem = system;
        }

        void IGameElement.UnbindGame()
        {
            this.GameSystem = null;
        }

        protected Lazy<T> GetComponentLazy<T>()
        {
            return new Lazy<T>(() => this.Entity.GetComponent<T>());
        }

        protected Lazy<T> GetServiceLazy<T>()
        {
            return new Lazy<T>(() => this.GameSystem.GetService<T>());
        }
    }
}