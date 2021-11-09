using System;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponAnimatorController : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private string animationName;

        [Header("Inject")]
        [SerializeField]
        private Animator attackAnimator;

        protected override void ProcessAttack()
        {
            this.attackAnimator.Play(this.animationName, -1, 0);
        }
    }
}