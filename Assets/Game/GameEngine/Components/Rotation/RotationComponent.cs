using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class RotationComponent : MonoBehaviour
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