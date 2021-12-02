using UnityEngine;

namespace Prototype.GUI
{
    [CreateAssetMenu(
        fileName = "MapRendererPipelineConfig",
        menuName = "GameInterface/Maps/New MapRendererPipelineConfig"
    )]
    public sealed class MapRenderPipelineConfig : MapRenderConfig
    {
        [SerializeField]
        private MapRenderConfig[] renderPipeline;
     
        public override IMapRenderer CreateRenderer()
        {
            var count = this.renderPipeline.Length;
            var orderedRenderers = new IMapRenderer[count];
            for (var i = 0; i < count; i++)
            {
                var config = this.renderPipeline[i];
                orderedRenderers[i] = config.CreateRenderer();
            }
            
            return new MapRendererPipeline(orderedRenderers);
        }
    }
}