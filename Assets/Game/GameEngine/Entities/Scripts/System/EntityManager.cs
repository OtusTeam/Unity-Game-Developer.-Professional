using System;
using System.Collections.Generic;

namespace Prototype.GameEngine
{
    public sealed class EntityManager : IEntityManager
    {
        public event Action<IEntity> OnEntityAdded;
        
        public event Action<IEntity> OnEntityRemoved;

        private readonly HashSet<IEntity> entitySet;

        private readonly List<IEntity> entityList;

        public EntityManager()
        {
            this.entitySet = new HashSet<IEntity>();
            this.entityList = new List<IEntity>();
        }

        public EntityManager(IEnumerable<IEntity> entities)
        {
            this.entitySet = new HashSet<IEntity>(entities);
        }

        public void AddEntity(IEntity entity)
        {
            if (this.entitySet.Add(entity))
            {
                this.entityList.Add(entity);
                this.OnEntityAdded?.Invoke(entity);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (this.entitySet.Remove(entity))
            {
                this.entityList.Remove(entity);
                this.OnEntityRemoved?.Invoke(entity);
            }
        }

        public IList<IEntity> GetEntities()
        {
            return this.entityList;
        }
    }
}