using UnityEngine;

namespace Prototype.GameInterface
{
    [CreateAssetMenu(
        fileName = "MapRenderEntitiesConfig",
        menuName = "GameInterface/Maps/New MapRenderEntitiesConfig"
    )]
    public sealed class MapRenderEntitiesConfig : MapRenderConfig
    {
        [SerializeField]
        private MapEntity entityPrefab;
        
        public override IMapRenderer CreateRenderer()
        {
            return new MapEntitiesRenderSystem(this.entityPrefab);
        }
    }
}