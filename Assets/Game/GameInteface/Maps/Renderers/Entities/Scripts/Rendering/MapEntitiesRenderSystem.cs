using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntitiesRenderSystem : GameController, IMapRenderer
    {
        private readonly MapEntitiesRender render;

        private readonly MapEntityLayerProvider layerProvider;

        private IEntityManager entityManager;

        public MapEntitiesRenderSystem(MapEntity prefab)
        {
            this.render = new MapEntitiesRender();
            this.layerProvider = new MapEntityLayerProvider(prefab);
        }

        public void Render(RectTransform layerTransform)
        {
            if (this.render == null)
            {
                DebugLogger.Error("Render is not initialized!");
                return;
            }

            var mapLayer = this.layerProvider.Provide(layerTransform);
            this.render.Render(mapLayer);
        }

        protected override bool Initialize(IGameSystem system)
        {
            return system.TryGetService(out this.entityManager);
        }

        protected override void OnReadyGame()
        {
            base.OnReadyGame();
            this.render.AddEntities(this.entityManager.GetEntities());
        }

        protected override void OnStartGame()
        {
            base.OnStartGame();
            this.entityManager.OnEntityAdded += this.OnEntityAdded;
            this.entityManager.OnEntityRemoved += this.OnEntityRemoved;
        }

        protected override void OnFinishGame()
        {
            base.OnFinishGame();

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