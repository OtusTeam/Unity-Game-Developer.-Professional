using System;
using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class EntityManager : MonoBehaviour, IEntityManager, IGameInitElement
    {
        private const string ENTITY_TAG = "Entity";

        public event Action<IEntity> OnEntityAdded;

        public event Action<IEntity> OnEntityRemoved;

        private readonly HashSet<IEntity> entities;

        private IGameSystem gameSystem;
        
        public IEnumerable<IEntity> GetEntities()
        {
            foreach (var entity in this.entities)
            {
                yield return entity;
            }
        }

        public void AddEntity(IEntity entity)
        {
            if (!this.entities.Add(entity))
            {
                return;
            }

            if (entity is IGameElement gameElement)
            {
                this.gameSystem.AddElement(gameElement);
            }

            this.OnEntityAdded?.Invoke(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            if (!this.entities.Remove(entity))
            {
                return;
            }

            if (entity is IGameElement gameElement)
            {
                this.gameSystem.RemoveElement(gameElement);
            }

            this.OnEntityRemoved?.Invoke(entity);
        }

        public EntityManager()
        {
            this.entities = new HashSet<IEntity>();
        }

        void IGameInitElement.InitGame(IGameSystem gameSystem)
        {
            this.gameSystem = gameSystem;
            this.InitializeEntities();
        }

        private void InitializeEntities()
        {
            var gameObjects = GameObject.FindGameObjectsWithTag(ENTITY_TAG);
            var count = gameObjects.Length;

            for (var i = 0; i < count; i++)
            {
                var gameObject = gameObjects[i];
                if (gameObject.TryGetComponent(out IEntity entity))
                {
                    this.AddEntity(entity);
                }
            }
        }
    }
}