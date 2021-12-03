using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntitiesRenderController : GameController, IMapRenderer
    {
        private MapEntitiesCullRenderer renderer;

        public void Render(MapLayer layer)
        {
            if (this.renderer != null)
            {
                this.renderer.Render(layer);
            }
            else
            {
                DebugLogger.Error("Render is not initialized!");
            }
        }
        
        protected override bool Initialize(IGameSystem system)
        {
            if (system.TryGetService(out IEntityManager entityManager))
            {
                this.renderer = new MapEntitiesCullRenderer(entityManager);
                return true;
            }

            return false;
        }

        protected override void OnStartGame()
        {
            base.OnStartGame();
            this.renderer.Start();
        }

        protected override void OnFinishGame()
        {
            base.OnFinishGame();
            this.renderer.Stop();
        }
    }
}