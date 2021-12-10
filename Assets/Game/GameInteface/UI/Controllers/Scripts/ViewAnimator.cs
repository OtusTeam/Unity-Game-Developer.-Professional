using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class ViewAnimator : MonoBehaviour
    {
        private const string OPEN_ANIMATION_NAME = "Open";

        private const string CLOSE_ANIMATION_NAME = "Close";

        private const string CLOSED_ANIMATION_MESSAGE = "closed";

        [SerializeField]
        private Popup popup;
        
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

        #region Events
        
        public void OnShow(object data)
        {
            if (this.hasAnimation)
            {
                this.animationReceiver.OnAnimationEvent += this.OnAnimationEvent;
                this.animator.Play(OPEN_ANIMATION_NAME, -1, 0);
            }
        }

        public void OnHide()
        {
            this.animationReceiver.OnAnimationEvent -= this.OnAnimationEvent;

            if (this.hasAnimation)
            {
                this.animator.Play(CLOSE_ANIMATION_NAME, -1, 0);
            }
            else
            {
                this.popup.Close();
            }
        }

        private void OnAnimationEvent(string message)
        {
            if (message == CLOSED_ANIMATION_MESSAGE)
            {
                this.popup.Close();
            }
        }
        
        #endregion
    }
}