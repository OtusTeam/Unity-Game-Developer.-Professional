using UnityEngine;

namespace Prototype.GUI
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