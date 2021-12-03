using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntitiesRenderer : GameController, IMapRenderer
    {
        private readonly MapEntityLayerRender render;

        private readonly MapEntityLayerProvider layerProvider;

        private MapEntityCullController cullController; 
        
        public MapEntitiesRenderer(MapEntity prefab)
        {
            this.render = new MapEntityLayerRender();
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
            if (!system.TryGetService(out IEntityManager entityManager))
            {
                return false;
            }

            this.cullController = new MapEntityCullController(entityManager, this.render);
            return true;
        }

        protected override void OnReadyGame()
        {
            base.OnReadyGame();
            this.cullController.Start();
        }

        protected override void OnFinishGame()
        {
            base.OnFinishGame();
            this.cullController.Finish();
        }
    }
}