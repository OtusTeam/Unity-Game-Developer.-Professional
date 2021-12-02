using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    public sealed class MonoEntityManager : MonoBehaviour, IEntityManager
    {
        private const string ENTITY_TAG = "Entity";

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

        private EntityManager entityManager;

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
        
        private void Awake()
        {
            this.entityManager = new EntityManager();
            this.InitializeEntities();
        }

        private void InitializeEntities()
        {
            var entitiesGO = GameObject.FindGameObjectsWithTag(ENTITY_TAG);
            var count = entitiesGO.Length;

            for (var i = 0; i < count; i++)
            {
                var entityGO = entitiesGO[i];
                if (entityGO.TryGetComponent(out IEntity entity))
                {
                    this.entityManager.AddEntity(entity);
                }
            }
        }
    }
}