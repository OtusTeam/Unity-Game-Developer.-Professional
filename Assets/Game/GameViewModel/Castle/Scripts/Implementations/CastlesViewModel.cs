using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.ViewModel
{
    public sealed class CastlesViewModel : MonoBehaviour, ICastlesManager,
        IGameInitElement
    {
        private IEntitiesManager entitiesManager;
        
        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.entitiesManager = system.GetService<IEntitiesManager>();
        }

        public ICastle GetCastle(int castleId)
        {
            var entity = this.entitiesManager.GetEntity(castleId);
            return new Castle(entity);
        }
    }
}