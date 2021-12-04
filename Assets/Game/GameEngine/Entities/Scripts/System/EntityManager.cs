using System;
using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class EntityManager : MonoBehaviour, IEntityManager, IGameElement
    {
        private const string ENTITY_TAG = "Entity";
        
        public event Action<IEntity> OnEntityAdded;
        
        public event Action<IEntity> OnEntityRemoved;

        private GameElementContainer entityContainer;

        public void AddEntity(IEntity entity)
        {
            if (this.entityContainer.AddElement(entity))
            {
                this.OnEntityAdded?.Invoke(entity);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (this.entityContainer.RemoveElement(entity))
            {
                this.OnEntityRemoved?.Invoke(entity);
            }
        }

        public IEnumerable<IEntity> GetEntities()
        {
            foreach (var entity in this.entityContainer)
            {
                yield return (IEntity) entity;
            }
        }
        
        private void Awake()
        {
            this.entityContainer = new GameElementContainer();
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
                    this.entityContainer.AddElement(entity);
                }
            }
        }

        void IGameElement.BindGame(IGameSystem system)
        {
            this.entityContainer.BindGame(system);
        }

        void IGameElement.Dispose()
        {
            this.entityContainer.Dispose();
        }
    }
}