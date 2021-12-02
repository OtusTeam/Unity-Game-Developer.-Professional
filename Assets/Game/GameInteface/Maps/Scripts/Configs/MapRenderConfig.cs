using UnityEngine;

namespace Prototype.GameInterface
{
    [CreateAssetMenu(
        fileName = "MapRenderConfig",
        menuName = "GameInterface/Map/New MapRenderConfig"
    )]
    public abstract class MapRenderConfig : ScriptableObject
    {
        public abstract IMapRenderer CreateRenderer();
    }
}