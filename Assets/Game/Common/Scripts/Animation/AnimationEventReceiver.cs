using System;
using UnityEngine;

namespace Prototype
{
    [RequireComponent(typeof(Animator))]
    public sealed class AnimationEventReceiver : MonoBehaviour
    {
        public event Action<string> OnAnimationEvent;

        private void AnimationEvent(string message)
        {
            this.OnAnimationEvent?.Invoke(message);
        }
    }
}