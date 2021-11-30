using System;
using DynamicObjects;
using Otus;
using Otus.GameEffects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Weapons
{
    public sealed class WeaponAttackBulletComponent : WeaponAttackComponent, IBulletListener
    {
        public override event Action OnAttack;

        [Inject]
        private IDynamicObject weapon;

        [Inject]
        private IBulletManager bulletManager;

        [Header("Bullet")]
        [SerializeField]
        private Transform firePoint;

        [Header("Damage")]
        [SerializeField]
        private bool hasDamage;

        [ShowIf("hasDamage")]
        [SerializeField]
        private int damage;

        [Header("Effects")]
        [SerializeField]
        private bool hasEffect;

        [ShowIf("hasEffect")]
        [SerializeField]
        private Effect effect;
        
        protected override void ProcessAttack()
        {
            this.bulletManager.LaunchBullet(
                position: this.firePoint.position,
                rotation: this.firePoint.rotation,
                direction: this.firePoint.forward,
                listener: this
            );

            this.OnAttack?.Invoke();
        }

        void IBulletListener.OnBulletCollided(Collider target)
        {
            if (this.hasDamage)
            {
                this.weapon
                    .GetProperty<IDynamicObject>(PropertyKey.PARENT)
                    .GetProperty<IDealDamageHandler>(PropertyKey.DEAL_DAMAGE_HANDLER)
                    .HandleDamage(target, this.damage);
            }
            
            if (this.hasEffect)
            {
                this.weapon
                    .GetProperty<IDynamicObject>(PropertyKey.PARENT)
                    .GetProperty<IEffectHandler>(PropertyKey.EFFECT_HANDLER)
                    .HandleEffect(target, this.effect);
            }
        }
    }
}