using System;
using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class EntityManager : MonoBehaviour, IEntityManager
    {
        private const string ENTITY_TAG = "Entity";
        
        public event Action<IEntity> OnEntityAdded;
        
        public event Action<IEntity> OnEntityRemoved;

        private GameElementSet entitySet;

        public void AddEntity(IEntity entity)
        {
            if (this.entitySet.AddElement(entity))
            {
                this.OnEntityAdded?.Invoke(entity);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (this.entitySet.RemoveElement(entity))
            {
                this.OnEntityRemoved?.Invoke(entity);
            }
        }

        public IEnumerable<IEntity> GetEntities()
        {
            foreach (var entity in this.entitySet)
            {
                yield return (IEntity) entity;
            }
        }
        
        private void Awake()
        {
            this.entitySet = new GameElementSet();
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
                    this.entitySet.AddElement(entity);
                }
            }
        }
    }
}