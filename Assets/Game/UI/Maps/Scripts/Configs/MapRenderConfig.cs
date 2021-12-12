using UnityEngine;

namespace Prototype.GameInterface
{
    public abstract class MapRenderConfig : ScriptableObject
    {
        public abstract IMapRenderer CreateRenderer();
    }
}