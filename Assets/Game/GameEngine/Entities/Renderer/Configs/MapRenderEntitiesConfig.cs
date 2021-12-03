using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    [CreateAssetMenu(
        fileName = "MapRenderEntitiesConfig",
        menuName = "GameInterface/Maps/New MapRenderEntitiesConfig"
    )]
    public class MapRenderEntitiesConfig : MapRenderConfig
    {
        public override IMapRenderer CreateRenderer()
        {
            return new MapEntitiesRendererElement();
        }
    }
}