using System;
using Otus;
using UnityEngine;

namespace Weapons
{
    public sealed class BulletWeaponController : Weapon, IBulletListener
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private BulletConfig config;

        [SerializeField]
        private Transform firePoint;

        [Header("Inject")]
        [SerializeField]
        private BulletManager bulletManager;

        [SerializeField]
        private DamageConditionChecker conditionProvider;

        protected override void ProcessAttack()
        {
            this.bulletManager.LaunchBullet(
                this.firePoint.position,
                this.firePoint.rotation,
                this.firePoint.forward,
                this
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
    }
}