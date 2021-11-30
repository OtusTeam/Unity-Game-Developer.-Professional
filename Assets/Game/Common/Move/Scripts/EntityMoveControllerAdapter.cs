using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class EntityMoveControllerAdapter : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [Inject]
        private EntityMoveController moveController;

        private float fixedDeltaTime;

        private void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;

            this.entity.AddMethod(ActionKey.MOVE, new MethodDelegate(this.OnMove));
            this.entity.AddMethod(ActionKey.LOCK_MOVE, new MethodDelegate(this.OnLockMove));
            this.entity.AddMethod(ActionKey.UNLOCK_MOVE, new MethodDelegate(this.OnUnlockMove));
            this.entity.AddMethod(ActionKey.ADD_MOVE_SPEED_MULTIPLIER, new MethodDelegate(this.OnAddMultiplier));
            this.entity.AddMethod(ActionKey.REMOVE_MOVE_SPEED_MULTIPLIER, new MethodDelegate(this.OnRemoveMultiplier));
        }

        #region Callbacks

        private object OnMove(object data)
        {
            var moveData = (MoveData) data;
            this.moveController.Move(moveData.direction, this.fixedDeltaTime);
            return null;
        }

        private object OnLockMove(object data)
        {
            this.moveController.IsLocked = true;
            return null;
        }

        private object OnUnlockMove(object data)
        {
            this.moveController.IsLocked = false;
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
    }
}