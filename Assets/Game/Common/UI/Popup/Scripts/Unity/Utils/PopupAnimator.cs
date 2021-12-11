using Prototype.Unity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Prototype.UI
{
    [RequireComponent(typeof(Animator))]
    public sealed class PopupAnimator : MonoBehaviour
    {
        private const string OPEN_ANIMATION_NAME = "Open";

        private const string CLOSE_ANIMATION_NAME = "Close";

        private const string CLOSED_ANIMATION_MESSAGE = "closed";

        [SerializeField]
        private Popup popup;

        [Header("Animation")]
        [SerializeField]
        private bool isEnable;

        [ShowIf("isEnable")]
        [SerializeField]
        private Animator animator;
        
        #region Events
        
        public void Show()
        {
            if (this.isEnable)
            {
                this.animator.Play(OPEN_ANIMATION_NAME, -1, 0);
            }
        }

        public void Hide()
        {
            if (this.isEnable)
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