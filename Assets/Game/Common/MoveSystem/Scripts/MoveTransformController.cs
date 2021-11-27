using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class MoveTransformController : MonoBehaviour, IMethodDelegate
    {
        [Inject]
        private IDynamicObject entity;
        
        [SerializeField]
        private float speed;

        [SerializeField]
        private Transform moveTransform;

        private float fixedDeltaTime;
        
        private void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
            this.entity.AddMethod(ActionKey.MOVE, this);
        }

        object IMethodDelegate.Invoke(object data)
        {
            if (data is MoveTransformData moveData)
            {
                this.moveTransform.position += this.speed * this.fixedDeltaTime * moveData.direction;
            }

            return null;
        }
    }
}