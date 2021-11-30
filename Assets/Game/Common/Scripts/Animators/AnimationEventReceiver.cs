using System;
using UnityEngine;

namespace Otus
{
    [RequireComponent(typeof(Animator))]
    public sealed class AnimationEventReceiver : MonoBehaviour
    {
        public event Action<string> OnAnimationEvent;
        
        [SerializeField]
        private Animator animator;

        private void AnimationEvent(string message)
        {
            this.OnAnimationEvent?.Invoke(message);
        }
    }
}