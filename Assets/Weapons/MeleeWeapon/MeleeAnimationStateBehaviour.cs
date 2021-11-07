using UnityEngine;

namespace Otus
{
    public sealed class MeleeAnimationStateBehaviour : StateMachineBehaviour
    {
        [SerializeField]
        private ObservableCollider meleeCollider;
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            this.meleeCollider.SetActive(false);
        }
    }
}