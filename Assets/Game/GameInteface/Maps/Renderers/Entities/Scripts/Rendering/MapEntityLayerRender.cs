using System.Collections.Generic;
using Prototype.GameEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityLayerRender
    {
        private readonly List<IMapEntityRenderComponent> addedEntities;

        private readonly List<IMapEntityRenderComponent> processingEntities;

        private readonly List<IMapEntityRenderComponent> removedEntities;

        private readonly List<IMapEntityRenderComponent> cache;

        public MapEntityLayerRender()
        {
            this.addedEntities = new List<IMapEntityRenderComponent>();
            this.processingEntities = new List<IMapEntityRenderComponent>();
            this.removedEntities = new List<IMapEntityRenderComponent>();
            this.cache = new List<IMapEntityRenderComponent>();
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
            if (entity.TryGetEntityComponent(out IMapEntityRenderComponent component))
            {
                this.addedEntities.Add(component);
                this.processingEntities.Add(component);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (entity.TryGetEntityComponent(out IMapEntityRenderComponent component))
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