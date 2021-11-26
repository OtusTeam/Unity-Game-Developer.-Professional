using UnityEngine;

namespace Otus
{
    public sealed class MoveTransformController : MonoBehaviour, IMoveController
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Transform moveTransform;

        private float fixedDeltaTime;
        
        private void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
        }

        public void Move(Vector3 direction)
        {
            this.moveTransform.position += this.speed * this.fixedDeltaTime * direction;
        }
    }
}