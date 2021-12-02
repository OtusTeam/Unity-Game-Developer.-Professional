using UnityEngine;

namespace Prototype.GameEngine
{
    public interface IPositionComponent
    {
        Vector3 GetPosition();

        void SetPosition(Vector3 position);
    }
    
    public sealed class PositionComponent : MonoEntityComponent, IPositionComponent
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