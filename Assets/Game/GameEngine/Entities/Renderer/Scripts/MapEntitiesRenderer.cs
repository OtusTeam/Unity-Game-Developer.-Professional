using System.Collections.Generic;
using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MapEntitiesRenderer : IMapRenderer
    {
        private readonly List<MapEntityRenderComponent> addedEntities;

        private readonly List<MapEntityRenderComponent> processingEntities;

        private readonly List<MapEntityRenderComponent> removedEntities;

        private readonly List<MapEntityRenderComponent> cache;

        public MapEntitiesRenderer()
        {
            this.addedEntities = new List<MapEntityRenderComponent>();
            this.processingEntities = new List<MapEntityRenderComponent>();
            this.removedEntities = new List<MapEntityRenderComponent>();
            this.cache = new List<MapEntityRenderComponent>();
        }

        public void AddEntities(IList<IEntity> entities)
        {
            for (int i = 0, count = entities.Count; i < count; i++)
            {
                var entity = entities[i];
                this.AddEntity(entity);
            }
        }
        
        public void AddEntity(IEntity entity)
        {
            if (entity.TryGetComponent(out MapEntityRenderComponent component))
            {
                this.addedEntities.Add(component);
                this.processingEntities.Add(component);
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            if (entity.TryGetComponent(out MapEntityRenderComponent component))
            {
                this.removedEntities.Add(component);
                this.processingEntities.Remove(component);
            }
        }

        public void ClearEntities()
        {
            this.removedEntities.AddRange(this.processingEntities);
            this.processingEntities.Clear();
        }

        public void Render(RectTransform plane)
        {
            this.StartRender(plane);
            this.UpdateRender(plane);
            this.EndRender(plane);
        }

        private void StartRender(RectTransform plane)
        {
            this.cache.Clear();
            this.cache.AddRange(this.addedEntities);
            this.addedEntities.Clear();

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var entity = this.cache[i];
                if (entity.TryGetComponent(out MapEntityRenderComponent component))
                {
                    component.OnStartRender(plane);
                }
            }
        }

        private void UpdateRender(RectTransform plane)
        {
            this.cache.Clear();
            this.cache.AddRange(this.processingEntities);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var entity = this.cache[i];
                if (entity.TryGetComponent(out MapEntityRenderComponent component))
                {
                    component.OnUpdateRender(plane);
                }
            }
        }

        private void EndRender(RectTransform plane)
        {
            this.cache.Clear();
            this.cache.AddRange(this.removedEntities);
            this.removedEntities.Clear();

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var entity = this.cache[i];
                if (entity.TryGetComponent(out MapEntityRenderComponent component))
                {
                    component.OnStartRender(plane);
                }
            }
        }
    }
}