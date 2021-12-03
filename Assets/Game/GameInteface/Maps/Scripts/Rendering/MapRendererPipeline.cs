using GameElements;

namespace Prototype.GameInterface
{
    public sealed class MapRendererPipeline : IMapRenderer, IGameElement
    {
        private readonly IMapRenderer[] orderedRenderers;

        public MapRendererPipeline(IMapRenderer[] orderedRenderers)
        {
            this.orderedRenderers = orderedRenderers;
        }
            
        public void Render(MapLayer layer)
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var render = this.orderedRenderers[i];
                render.Render(layer);
            }
        }
            
        void IGameElement.BindGame(IGameSystem system)
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var renderer = this.orderedRenderers[i];
                if (renderer is IGameElement gameElement)
                {
                    gameElement.BindGame(system);
                }
            }
        }

        void IGameElement.UnbindGame()
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var renderer = this.orderedRenderers[i];
                if (renderer is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }
            }
        }
    }
}