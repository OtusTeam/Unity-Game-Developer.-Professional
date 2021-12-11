using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntitiesRenderer : IMapRenderer,
        IGameInitElement,
        IGameReadyElement,
        IGameFinishElement
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
            var mapLayer = this.layerProvider.Provide(layerTransform);
            this.layerRender.Render(mapLayer);
        }
        
        void IGameInitElement.InitGame(IGameSystem system)
        {
            var entityManager = system.GetService<IEntitiesManager>();
            this.cullController = new MapEntityCullController(entityManager, this.layerRender);
        }

        void IGameReadyElement.ReadyGame(IGameSystem system)
        {
            this.cullController.Start();
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.cullController.Finish();
        }
    }
}