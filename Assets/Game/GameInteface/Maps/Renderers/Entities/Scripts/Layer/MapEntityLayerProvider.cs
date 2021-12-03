using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntityLayerProvider
    {
        private readonly MapEntity prefab;

        private RectTransform currentLayerTransform;
        
        private MapEntityLayer currentEntityLayer;

        public MapEntityLayerProvider(MapEntity prefab)
        {
            this.prefab = prefab;
        }

        public IMapEntityLayer Provide(RectTransform layerTransform)
        {
            if (ReferenceEquals(this.currentLayerTransform, layerTransform))
            {
                return this.currentEntityLayer;
            }
            
            this.currentLayerTransform = layerTransform;
            this.currentEntityLayer = new MapEntityLayer(layerTransform, this.prefab);
            return this.currentEntityLayer;
        }
    }
}