using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MapEntitiesCullRenderer : IMapRenderer
    {
        private readonly IEntityManager entityManager;

        private readonly MapEntitiesRenderer renderer;

        public MapEntitiesCullRenderer(IEntityManager entityManager)
        {
            this.entityManager = entityManager;
            this.renderer = new MapEntitiesRenderer();
        }

        public void Start()
        {
            this.entityManager.OnEntityAdded += this.OnEntityAdded;
            this.entityManager.OnEntityRemoved += this.OnEntityRemoved;

            var entities = this.entityManager.GetEntities();
            this.renderer.AddEntities(entities);
        }

        public void Stop()
        {
            this.entityManager.OnEntityAdded -= this.OnEntityAdded;
            this.entityManager.OnEntityRemoved -= this.OnEntityRemoved;
            this.renderer.ClearEntities();
        }

        public void Render(RectTransform plane)
        {
            this.renderer.Render(plane);
        }

        #region Callbacks

        private void OnEntityAdded(IEntity entity)
        {
            this.renderer.AddEntity(entity);
        }

        private void OnEntityRemoved(IEntity entity)
        {
            this.renderer.RemoveEntity(entity);
        }

        #endregion
    }
}