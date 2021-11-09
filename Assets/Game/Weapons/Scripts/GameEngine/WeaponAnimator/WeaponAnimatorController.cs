using System;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponAnimatorController : Weapon, IPropertyInjector
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private string animationName;

        private Animator attackAnimator;

        protected override void ProcessAttack()
        {
            this.attackAnimator.Play(this.animationName, -1, 0);
        }

        void IPropertyInjector.Set(IPropertyProvider provider)
        {
            this.attackAnimator = provider.Get<Animator>(PropertyId.ANIMATOR);
        }
    }
}