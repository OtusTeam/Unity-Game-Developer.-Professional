using Prototype.GameEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityCullController
    {
        private readonly IEntitiesManager entitiesManager;

        private readonly MapEntityLayerRender render;

        public MapEntityCullController(IEntitiesManager entitiesManager, MapEntityLayerRender render)
        {
            this.entitiesManager = entitiesManager;
            this.render = render;
        }

        public void Start()
        {
            this.render.AddEntities(this.entitiesManager.GetEntities());
            this.entitiesManager.OnEntityAdded += this.OnEntitiesAdded;
            this.entitiesManager.OnEntityRemoved += this.OnEntitiesRemoved;
        }

        public void Finish()
        {
            this.entitiesManager.OnEntityAdded -= this.OnEntitiesAdded;
            this.entitiesManager.OnEntityRemoved -= this.OnEntitiesRemoved;
        }

        #region Callbacks

        private void OnEntitiesAdded(IEntity entity)
        {
            this.render.AddEntity(entity);
        }

        private void OnEntitiesRemoved(IEntity entity)
        {
            this.render.RemoveEntity(entity);
        }

        #endregion
    }
}