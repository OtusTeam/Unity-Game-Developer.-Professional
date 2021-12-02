using System;
using System.Collections.Generic;

namespace Prototype.GameEngine
{
    public sealed class EntityManager : IEntityManager
    {
        public event Action<IEntity> OnEntityAdded;
        
        public event Action<IEntity> OnEntityRemoved;

        private readonly HashSet<IEntity> entities;

        public EntityManager()
        {
            this.entities = new HashSet<IEntity>();
        }

        public EntityManager(IEnumerable<IEntity> entities)
        {
            this.entities = new HashSet<IEntity>(entities);
        }

        public void AddEntity(IEntity entity)
        {
            if (this.entities.Add(entity))
            {
                this.OnEntityAdded?.Invoke(entity);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (this.entities.Remove(entity))
            {
                this.OnEntityRemoved?.Invoke(entity);
            }
        }

        public IEnumerable<IEntity> GetEntities()
        {
            foreach (var entity in this.entities)
            {
                yield return entity;
            }
        }
    }
}