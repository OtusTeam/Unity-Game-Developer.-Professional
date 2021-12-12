using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PlayerEntityService : MonoBehaviour
    {
        [SerializeField]
        private Entity entity;

        public IEntity GetPlayerEntity()
        {
            return this.entity;
        }
    }
}