using UnityEngine;

namespace Prototype.GUI
{
    public sealed class MapRenderPipeline : MonoBehaviour, IMapRenderer
    {
        [SerializeField]
        private MapRenderPipelineConfig config;

        private IMapRenderer[] orderedRenderers;

        private void Awake()
        {
            this.orderedRenderers = this.config.LoadOrderedRenderers();
        }

        void IMapRenderer.Render(Transform plane)
        {
            for (int i = 0, count = this.orderedRenderers.Length; i < count; i++)
            {
                var render = this.orderedRenderers[i];
                render.Render(plane);
            }
        }
    }
}