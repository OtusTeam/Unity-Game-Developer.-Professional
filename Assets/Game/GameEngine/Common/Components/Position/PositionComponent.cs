using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PositionComponent : MonoEntityComponent
    {
        [SerializeField]
        private Transform target;

        public WorldVector GetPosition()
        {
            var position = this.target.position;
            return new WorldVector(position.x, position.z);
        }

        public void SetPosition(WorldVector position)
        {
            this.target.position = new Vector3(position.x, 0.0f, position.z);
        }
    }
}