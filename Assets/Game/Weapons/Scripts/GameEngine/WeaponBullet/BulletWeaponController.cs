using System;
using Otus;
using UnityEngine;

namespace Weapons
{
    public sealed class BulletWeaponController : Weapon, IBulletListener, IPropertyInjector
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private BulletConfig config;

        [SerializeField]
        private Transform firePoint;
        
        private IBulletManager bulletManager;

        private DamageConditionChecker conditionProvider;

        protected override void ProcessAttack()
        {
            this.bulletManager.LaunchBullet(
                position: this.firePoint.position,
                rotation: this.firePoint.rotation,
                direction: this.firePoint.forward,
                listener: this
            );

            this.OnAttack?.Invoke(this);
        }

        void IBulletListener.OnBulletCollided(Collider target)
        {
            if (target.TryGetComponent(out DamageComponent damageComponent) && 
                this.conditionProvider.CanTakeDamage(damageComponent))
            {
                damageComponent.TakeDamage(this.config.damage);
            }
        }

        void IPropertyInjector.Set(IPropertyProvider provider)
        {
            this.bulletManager = provider.Get<IBulletManager>(PropertyId.BULLET_SYSTEM);
            this.conditionProvider = provider.Get<DamageConditionChecker>(PropertyId.DAMAGE_CHECKER);
        }
    }
}