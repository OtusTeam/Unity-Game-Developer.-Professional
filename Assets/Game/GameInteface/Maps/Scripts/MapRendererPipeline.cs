using GameElements;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapRendererPipeline : IMapRenderer, IGameElement
    {
        private readonly IMapRenderer[] orderedRenderers;

        public MapRendererPipeline(IMapRenderer[] orderedRenderers)
        {
            this.orderedRenderers = orderedRenderers;
        }
            
        public void Render(Transform plane)
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var render = this.orderedRenderers[i];
                render.Render(plane);
            }
        }
            
        void IGameElement.Setup(IGameSystem system)
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var renderer = this.orderedRenderers[i];
                if (renderer is IGameElement gameElement)
                {
                    gameElement.Setup(system);
                }
            }
        }

        void IGameElement.Dispose()
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var renderer = this.orderedRenderers[i];
                if (renderer is IGameElement gameElement)
                {
                    gameElement.Dispose();
                }
            }
        }
    }
}