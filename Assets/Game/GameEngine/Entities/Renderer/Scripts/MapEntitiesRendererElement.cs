using GameElements;
using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MapEntitiesRendererElement : GameController, IMapRenderer
    {
        private MapEntitiesRenderController renderer;

        public void Render(RectTransform plane)
        {
            if (this.renderer != null)
            {
                this.renderer.Render(plane);
            }
        }
        
        protected override bool Initialize(IGameSystem system)
        {
            if (system.TryGetElement(out IEntityManager entityManager))
            {
                this.renderer = new MapEntitiesRenderController(entityManager);
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