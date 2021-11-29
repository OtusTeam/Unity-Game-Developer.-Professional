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

        [Header("Bullet")]
        [SerializeField]
        private BulletConfig config;

        [SerializeField]
        private Transform firePoint;

        [Inject]
        private IBulletManager bulletManager;

        [Header("Damage")]
        [SerializeField]
        private bool hasDamage;

        [Header("Effects")]
        [SerializeField]
        private bool hasEffect;

        [ShowIf("hasEffect")]
        [SerializeField]
        private EffectHandler effectHandler;

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
                    .GetProperty<DealDamageHandler>(PropertyKey.DEAL_DAMAGE_HANDLER)
                    .HandleDamage(target, this.config.damage);
            }
            
            if (this.hasEffect)
            {
                this.effectHandler.HandleEffect(target);
            }
        }
    }
}