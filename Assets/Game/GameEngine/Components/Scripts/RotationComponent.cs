using UnityEngine;

namespace GameEngine
{
    public interface IRotationComponent
    {
        float GetRotation();

        void SetRotation(float rotation);
    }
    
    public sealed class RotationComponent : MonoBehaviour, IRotationComponent
    {
        [SerializeField]
        private Transform targetTransform;

        public float GetRotation()
        {
            return this.targetTransform.eulerAngles.y;
        }

        public void SetRotation(float rotation)
        {
            this.targetTransform.eulerAngles = new Vector3(0, rotation, 0);
        }
    }
}