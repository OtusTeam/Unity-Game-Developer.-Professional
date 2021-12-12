using System.Collections.Generic;
using Prototype.GameEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityLayerRender
    {
        private readonly List<IMapItemComponent> addedEntities;

        private readonly List<IMapItemComponent> processingEntities;

        private readonly List<IMapItemComponent> removedEntities;

        private readonly List<IMapItemComponent> cache;

        public MapEntityLayerRender()
        {
            this.addedEntities = new List<IMapItemComponent>();
            this.processingEntities = new List<IMapItemComponent>();
            this.removedEntities = new List<IMapItemComponent>();
            this.cache = new List<IMapItemComponent>();
        }

        public void AddEntities(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.AddEntity(entity);
            }
        }

        public void AddEntity(IEntity entity)
        {
            if (entity.TryGetEntityComponent(out IMapItemComponent component))
            {
                this.addedEntities.Add(component);
                this.processingEntities.Add(component);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (entity.TryGetEntityComponent(out IMapItemComponent component))
            {
                this.removedEntities.Add(component);
                this.processingEntities.Remove(component);
            }
        }

        public void Render(IMapEntityLayer entityLayer)
        {
            this.StartRender(entityLayer);
            this.UpdateRender(entityLayer);
            this.EndRender(entityLayer);
        }

        private void StartRender(IMapEntityLayer layer)
        {
            this.cache.Clear();
            this.cache.AddRange(this.addedEntities);
            this.addedEntities.Clear();

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var entity = this.cache[i];
                entity.StartRender(layer);
            }
        }

        private void UpdateRender(IMapEntityLayer layer)
        {
            this.cache.Clear();
            this.cache.AddRange(this.processingEntities);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var entity = this.cache[i];
                entity.UpdateRender(layer);
            }
        }

        private void EndRender(IMapEntityLayer layer)
        {
            this.cache.Clear();
            this.cache.AddRange(this.removedEntities);
            this.removedEntities.Clear();

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var entity = this.cache[i];
                entity.FinishRender(layer);
            }
        }
    }
}