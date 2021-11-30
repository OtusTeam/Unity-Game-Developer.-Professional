using System;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponAttackAnimationComponent : WeaponAttackComponent
    {
        public override event Action OnAttack;

        [SerializeField]
        private string animationName;

        [Inject]
        private IDynamicObject weapon;

        protected override void ProcessAttack()
        {
            this.weapon
                .GetProperty<IDynamicObject>(PropertyKey.PARENT)
                .GetProperty<Animator>(PropertyKey.ANIMATOR)
                .Play(this.animationName, -1, 0);
            
            this.OnAttack?.Invoke();
        }
    }
}