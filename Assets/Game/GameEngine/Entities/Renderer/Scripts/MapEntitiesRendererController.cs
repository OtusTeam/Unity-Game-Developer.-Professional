using GameElements;
using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MapEntitiesRendererController : GameController, IMapRenderer
    {
        private IEntityManager entityManager;

        private bool isInitialized;

        protected override bool Initialize(IGameSystem system)
        {
            if (!system.TryGetElement(out this.entityManager))
            {
                DebugLogger.Error("Entity Manager is not found!");
                return false;
            }

            this.isInitialized = true;
            return true;
        }
        
        public void Render(RectTransform plane)
        {
            if (!this.isInitialized)
            {
                return;
            }

            var entities = this.entityManager.GetEntities();
            for (int i = 0, count = entities.Count; i < count; i++)
            {
                var entity = entities[i];
                if (entity.TryGetComponent(out MapEntityRenderComponent component))
                {
                    component.OnUpdate(plane);
                }
            }
        }
    }
}