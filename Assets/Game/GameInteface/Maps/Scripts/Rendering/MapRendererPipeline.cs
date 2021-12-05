using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapRendererPipeline : IMapRenderer, IGameElementGroup
    {
        private readonly IMapRenderer[] orderedRenderers;

        public MapRendererPipeline(IMapRenderer[] orderedRenderers)
        {
            this.orderedRenderers = orderedRenderers;
        }
            
        public void Render(RectTransform layerTransform)
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var render = this.orderedRenderers[i];
                render.Render(layerTransform);
            }
        }
        
        public IEnumerable<IGameElement> GetElements()
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var renderer = this.orderedRenderers[i];
                if (renderer is IGameElement gameElement)
                {
                    yield return gameElement;
                }
            }
        }
    }
}