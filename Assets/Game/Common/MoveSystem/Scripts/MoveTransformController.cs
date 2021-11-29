using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    //Можно разбить на паттерн компоновщик
    public sealed class MoveTransformController : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [SerializeField]
        private float speed;

        [SerializeField]
        private Transform moveTransform;

        [SerializeField]
        private bool moveEnabled = true;

        [SerializeField]
        private bool moveLocked;

        private float fixedDeltaTime;

        private void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
            this.entity.AddMethod(ActionKey.MOVE, new MethodDelegate(this.OnMove));
            this.entity.AddMethod(ActionKey.LOCK_MOVE, new MethodDelegate(this.OnLockMove));
            this.entity.AddMethod(ActionKey.UNLOCK_MOVE, new MethodDelegate(this.OnUnlockMove));
        }

        #region Callbacks

        private object OnMove(object data)
        {
            if (!this.moveEnabled)
            {
                return null;
            }

            if (this.moveLocked)
            {
                return null;
            }

            if (data is MoveTransformData moveData)
            {
                this.moveTransform.position += this.speed * this.fixedDeltaTime * moveData.direction;
            }

            return null;
        }

        private object OnLockMove(object data)
        {
            this.moveEnabled = false;
            return null;
        }

        private object OnUnlockMove(object data)
        {
            this.moveEnabled = true;
            return null;
        }

        #endregion

        //Unity event
        public void SetEnableMove(bool isMoveEnabled)
        {
            this.moveEnabled = isMoveEnabled;
        }
    }
}