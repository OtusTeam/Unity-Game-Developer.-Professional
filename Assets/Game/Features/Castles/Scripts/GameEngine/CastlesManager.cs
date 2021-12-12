using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class CastlesManager : MonoBehaviour, ICastlesManager
    {
        [SerializeField]
        private EntitiesManager entitiesManager;

        public ICastle GetCastle(int castleId)
        {
            var entity = this.entitiesManager.GetEntity(castleId);
            return new Castle(entity);
        }
    }
}