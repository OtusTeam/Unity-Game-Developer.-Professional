using GameElements;
using Prototype.GameInterface;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MapEntitiesRenderer : IMapRenderer, IGameElement
    {
        private IGameSystem gameSystem;

        public void Render(Transform plane)
        {
            if (!this.gameSystem.TryGetElement<IEntityManager>(out var entityManager))
            {
                Debug.LogError("Entity Manager is not found!");
                return;
            }

            var entities = entityManager.GetEntities();
            for (int i = 0, count = entities.Count; i < count; i++)
            {
                var entity = entities[i];
                if (entity.TryGetComponent(out IMapRenderer renderer))
                {
                    renderer.Render(plane);
                }
            }
        }

        void IGameElement.Setup(IGameSystem system)
        {
            this.gameSystem = system;
        }

        void IGameElement.Dispose()
        {
            this.gameSystem = null;
        }
    }
}