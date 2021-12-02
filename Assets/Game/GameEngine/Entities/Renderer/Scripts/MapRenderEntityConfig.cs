using Prototype.GUI;

namespace Prototype.GameEngine
{
    public class MapRenderEntityConfig : MapRenderConfig
    {
        public override IMapRenderer CreateRenderer()
        {
            return new MapEntitiesRenderer();
        }
    }
}