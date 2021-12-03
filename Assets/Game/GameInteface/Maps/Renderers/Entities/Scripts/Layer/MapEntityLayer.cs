using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityLayer : MapLayer
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private MapEntity entityPrefab;
        
        private IPool<MapEntity> entityPool;

        private Dictionary<int, MapEntity> activeEntities;
        
        public void AddEntity(Args args, out int entityId)
        {
            var entity = this.entityPool.Pop();
            entityId = entity.Id;
            this.activeEntities.Add(entityId, entity);

            this.UpdateEntity(entity, args);
        }

        public void UpdateEntity(int entityId, Args args)
        {
            if (this.activeEntities.TryGetValue(entityId, out var entity))
            {
                this.UpdateEntity(entity, args);
            }
        }
        
        public void RemoveEntity(int entityId)
        {
            if (this.activeEntities.TryGetValue(entityId, out var entity))
            {
                this.activeEntities.Remove(entityId);
                this.entityPool.Push(entity);
            }
        }
        
        private void Awake()
        {
            this.entityPool = new EntityPool(new EntityFactory(this.entityPrefab, this.container));
            this.activeEntities = new Dictionary<int, MapEntity>();
        }

        private void UpdateEntity(MapEntity entity, Args args)
        {
            var screenPosition = this.TransformPosition(args.normalizedPosition);
            var screenSize = this.TransformVector(args.normalizedSize);

            entity.SetPosition(screenPosition);
            entity.SetSize(screenSize);
            entity.SetColor(args.color);
            entity.SetIcon(args.icon);
        }

        public struct Args
        {
            public Vector2 normalizedPosition;

            public Vector2 normalizedSize;

            public Color color;

            public Sprite icon;
        }

        private sealed class EntityFactory : IFactory<MapEntity>
        {
            private readonly MapEntity prefab;

            private readonly Transform container;

            private int idCounter;

            public EntityFactory(MapEntity prefab, Transform container)
            {
                this.prefab = prefab;
                this.container = container;
            }

            public MapEntity Instantiate()
            {
                var entity = GameObject.Instantiate(this.prefab, this.container);
                entity.SetId(++this.idCounter);
                entity.gameObject.SetActive(false);
                return entity;
            }
        }

        private sealed class EntityPool : IPool<MapEntity>
        {
            private readonly IPool<MapEntity> pool;

            public EntityPool(IFactory<MapEntity> factory)
            {
                this.pool = new Pool<MapEntity>(factory, initialSize: 2);
            }

            public MapEntity Pop()
            {
                var entity = this.pool.Pop();
                entity.gameObject.SetActive(true);
                return entity;
            }

            public void Push(MapEntity obj)
            {
                obj.gameObject.SetActive(false);
                this.pool.Push(obj);
            }
        }
    }
}