using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class EntityManager : MonoBehaviour, IEntityManager
    {
        private const string ENTITY_TAG = "Entity";

        public event Action<IEntity> OnEntityAdded;

        public event Action<IEntity> OnEntityRemoved;

        private readonly Dictionary<int, IEntity> entityMap;

        private int idCounter;

        public IEntity GetEntity(int id)
        {
            return this.entityMap[id];
        }

        public IEnumerable<IEntity> GetEntities()
        {
            foreach (var pair in this.entityMap)
            {
                yield return pair.Value;
            }
        }

        public void AddEntity(IEntity entity)
        {
            if (this.entityMap.ContainsKey(entity.Id))
            {
                throw new Exception($"Entity with id {entity.Id} is already added!");
            }

            var id = ++this.idCounter;
            entity.SetupId(id);
            entity.SetActive(true);
            
            this.entityMap.Add(id, entity);
            this.OnEntityAdded?.Invoke(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            if (this.entityMap.Remove(entity.Id))
            {
                entity.SetActive(false);
                this.OnEntityRemoved?.Invoke(entity);
            }
        }

        public EntityManager()
        {
            this.entityMap = new Dictionary<int, IEntity>();
        }

        private void Awake()
        {
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