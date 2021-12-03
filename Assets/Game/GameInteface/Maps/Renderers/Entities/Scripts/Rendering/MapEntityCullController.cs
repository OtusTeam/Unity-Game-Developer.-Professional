using Prototype.GameEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityCullController
    {
        private readonly IEntityManager entityManager;

        private readonly MapEntityLayerRender render;

        public MapEntityCullController(IEntityManager entityManager, MapEntityLayerRender render)
        {
            this.entityManager = entityManager;
            this.render = render;
        }

        public void Start()
        {
            this.render.AddEntities(this.entityManager.GetEntities());
            this.entityManager.OnEntityAdded += this.OnEntityAdded;
            this.entityManager.OnEntityRemoved += this.OnEntityRemoved;
        }

        public void Finish()
        {
            this.entityManager.OnEntityAdded -= this.OnEntityAdded;
            this.entityManager.OnEntityRemoved -= this.OnEntityRemoved;
        }

        #region Callbacks

        private void OnEntityAdded(IEntity entity)
        {
            this.render.AddEntity(entity);
        }

        private void OnEntityRemoved(IEntity entity)
        {
            this.render.RemoveEntity(entity);
        }

        #endregion
    }
}