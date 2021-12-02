using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PositionComponent : MonoEntityComponent
    {
        [SerializeField]
        private Transform target;

        public Vector3 GetPosition()
        {
            return this.target.position;
        }

        public void SetPosition(Vector3 position)
        {
            this.target.position = position;
        }
    }
}