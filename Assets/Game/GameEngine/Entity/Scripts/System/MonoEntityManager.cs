using System;
using System.Collections.Generic;
using GameElements.Unity;

namespace GameEngine
{
    public sealed class MonoEntityManager : UnityGameElement, IEntityManager
    {
        public event Action<IEntity> OnEntityAdded
        {
            add { this.entityManager.OnEntityAdded += value; }
            remove { this.entityManager.OnEntityAdded -= value; }
        }

        public event Action<IEntity> OnEntityRemoved
        {
            add { this.entityManager.OnEntityAdded += value; }
            remove { this.entityManager.OnEntityRemoved -= value; }
        }

        private readonly EntityManager entityManager;

        public MonoEntityManager()
        {
            this.entityManager = new EntityManager();
        }

        public void AddEntity(IEntity entity)
        {
            this.entityManager.AddEntity(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            this.entityManager.RemoveEntity(entity);
        }

        public IEnumerable<IEntity> GetEntities()
        {
            return this.entityManager.GetEntities();
        }
    }
}