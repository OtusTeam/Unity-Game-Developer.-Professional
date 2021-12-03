using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntitiesRenderer : GameController, IMapRenderer
    {
        private readonly MapEntityLayerRender layerRender;

        private readonly MapEntityLayerProvider layerProvider;

        private MapEntityCullController cullController; 
        
        public MapEntitiesRenderer(MapEntity prefab)
        {
            this.layerRender = new MapEntityLayerRender();
            this.layerProvider = new MapEntityLayerProvider(prefab);
        }

        public void Render(RectTransform layerTransform)
        {
            if (this.layerRender == null)
            {
                DebugLogger.Error("Render is not initialized!");
                return;
            }

            var mapLayer = this.layerProvider.Provide(layerTransform);
            this.layerRender.Render(mapLayer);
        }

        protected override bool Initialize(IGameSystem system)
        {
            if (!system.TryGetService(out IEntityManager entityManager))
            {
                return false;
            }

            this.cullController = new MapEntityCullController(entityManager, this.layerRender);
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