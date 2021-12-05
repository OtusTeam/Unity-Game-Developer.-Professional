using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.UI
{
    public abstract class PopupAnimated : Popup
    {
        private const string OPEN_ANIMATION_NAME = "Open";

        private const string CLOSE_ANIMATION_NAME = "Close";

        private const string OPENED_ANIMATION_MESSAGE = "opened";

        private const string CLOSED_ANIMATION_MESSAGE = "closed";
        
        [Space]
        [Header("Animation")]
        [SerializeField]
        private bool hasAnimation = true;

        [ShowIf("hasAnimation")]
        [SerializeField]
        private Animator animator;

        [ShowIf("hasAnimation")]
        [SerializeField]
        private AnimationEventReceiver animationReceiver;

        protected override void OnShow(object data)
        {
            if (this.hasAnimation)
            {
                this.animationReceiver.OnAnimationEvent += this.OnAnimationEvent;
                this.animator.Play(OPEN_ANIMATION_NAME, -1, 0);
            }
        }
        
        protected virtual void OnShowAnimationFinished()
        {
        }

        protected override void OnHide()
        {
            if (this.hasAnimation)
            {
                this.animationReceiver.OnAnimationEvent -= this.OnAnimationEvent;
            }
        }

        protected void CloseWithAnimation()
        {
            if (this.hasAnimation)
            {
                this.animator.Play(CLOSE_ANIMATION_NAME, -1, 0);
            }
            else
            {
                this.Close();
            }
        }

        #region AnimationEvent

        private void OnAnimationEvent(string message)
        {
            if (message == OPENED_ANIMATION_MESSAGE)
            {
                this.OnShowAnimationFinished();
            }
            else if (message == CLOSED_ANIMATION_MESSAGE)
            {
                this.Close();
            }
        }
        
        #endregion
    }
}