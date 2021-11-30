using System;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public class EntityMoveControllerAdapter : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [SerializeField]
        private Parameters parameters;

        private EntityMoveController moveController;

        private float fixedDeltaTime;

        private void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
            this.moveController = new EntityMoveController(this.parameters.moveTransform, this.parameters.moveSpeed);

            this.entity.AddMethod(ActionKey.MOVE, new MethodDelegate(this.OnMove));
            this.entity.AddMethod(ActionKey.LOCK_MOVE, new MethodDelegate(this.OnLockMove));
            this.entity.AddMethod(ActionKey.UNLOCK_MOVE, new MethodDelegate(this.OnUnlockMove));
            this.entity.AddMethod(ActionKey.ADD_MOVE_MULTIPLIER, new MethodDelegate(this.OnAddMultiplier));
            this.entity.AddMethod(ActionKey.REMOVE_MOVE_MULTIPLIER, new MethodDelegate(this.OnRemoveMultiplier));
        }

        #region Callbacks

        private object OnMove(object data)
        {
            if (!this.parameters.moveEnabled)
            {
                return null;
            }

            if (this.parameters.moveLocked)
            {
                return null;
            }

            var moveData = (MoveData) data;
            this.moveController.Move(moveData.direction, this.fixedDeltaTime);
            return null;
        }

        private object OnLockMove(object data)
        {
            this.parameters.moveLocked = true;
            return null;
        }

        private object OnUnlockMove(object data)
        {
            this.parameters.moveLocked = false;
            return null;
        }

        private object OnAddMultiplier(object data)
        {
            var multiplier = (IMultiplier<float>) data;
            this.moveController.AddSpeedMultiplier(multiplier);
            return null;
        }

        private object OnRemoveMultiplier(object data)
        {
            var multiplier = (IMultiplier<float>) data;
            this.moveController.RemoveSpeedMultiplier(multiplier);
            return null;
        }

        #endregion

        public void SetEnableMove(bool isMoveEnabled)
        {
            this.parameters.moveEnabled = isMoveEnabled;
        }

        [Serializable]
        private sealed class Parameters
        {
            [SerializeField]
            public bool moveEnabled = true;

            [SerializeField]
            public bool moveLocked;

            [SerializeField]
            public float moveSpeed;

            [SerializeField]
            public Transform moveTransform;
        }
    }
}