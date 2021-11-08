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

        [SerializeField]
        private bool isActive;

        [Header("Inject")]
        [SerializeField]
        private BulletManager bulletManager;

        [SerializeField]
        private DamageConditionChecker conditionProvider;

        public override void Attack()
        {
            if (!this.isActive)
            {
                return;
            }

            this.bulletManager.LaunchBullet(
                this.firePoint.position,
                this.firePoint.rotation,
                this.firePoint.forward,
                this
            );

            this.OnAttack?.Invoke(this);
        }

        public override bool CanAttack()
        {
            return this.isActive;
        }

        public override void SetActive(bool isActive)
        {
            this.isActive = isActive;
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