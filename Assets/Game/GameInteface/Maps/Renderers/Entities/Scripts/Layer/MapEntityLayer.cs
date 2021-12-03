using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityLayer : MapLayer, IMapEntityLayer
    {
        private readonly IPool<MapEntity> entityPool;

        private readonly Dictionary<int, MapEntity> activeEntities;

        public MapEntityLayer(RectTransform transform, MapEntity prefab) : base(transform)
        {
            this.activeEntities = new Dictionary<int, MapEntity>();
            this.entityPool = new EntityPool(new EntityFactory(prefab, transform));
        }

        public void AddEntity(MapEntityArgs args, out int entityId)
        {
            var entity = this.entityPool.Pop();
            entityId = entity.Id;
            this.activeEntities.Add(entityId, entity);

            this.UpdateEntity(entity, args);
        }

        public void UpdateEntity(int entityId, MapEntityArgs args)
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

        private void UpdateEntity(MapEntity entity, MapEntityArgs args)
        {
            var screenPosition = this.TransformPosition(args.normalizedPosition);
            var screenSize = this.TransformVector(args.normalizedSize);

            entity.SetPosition(screenPosition);
            entity.SetSize(screenSize);
            entity.SetColor(args.color);
            entity.SetIcon(args.icon);
        }


        /// <summary>
        ///     <para>Entity factory.</para>
        /// </summary>
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

        /// <summary>
        ///     <para>Entity pool.</para>
        /// </summary>
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