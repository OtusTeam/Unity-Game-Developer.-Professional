using UnityEngine;

namespace Otus
{
    public interface IEntityAnimator
    {
        void Animate(string animationName);
    }
    
    public sealed class EntityAnimator : MonoBehaviour, IEntityAnimator
    {
        [SerializeField]
        private Animator animator;

        public void Animate(string animationName)
        {
            this.animator.Play(animationName, -1, 0);
        }
    }
}