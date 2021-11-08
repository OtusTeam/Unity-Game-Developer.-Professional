using System;
using Otus;
using UnityEngine;

namespace Weapons
{
    public sealed class BulletWeaponController : WeaponComponent
    {
        public override event Action<IWeapon> OnAttack;

        [SerializeField]
        private int damage;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private bool isActive;
        
        [Header("Inject")]
        [SerializeField]
        private BulletManager bulletManager;

        public override void Attack()
        {
            if (!this.isActive)
            {
                return;
            }
            
            var position = this.firePoint.position;
            var rotation = this.firePoint.rotation;
            var direction = this.firePoint.forward;
            this.bulletManager.LaunchBullet(position, rotation, direction);
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
        
        private void OnEnable()
        {
            this.bulletManager.OnBulletReached += this.OnBulletReached;
        }

        private void OnDisable()
        {
            this.bulletManager.OnBulletReached -= this.OnBulletReached;
        }

        private void OnBulletReached(Collider target)
        {
            if (target.TryGetComponent(out Eneny eneny))
            {
                eneny.TakeDamage(this.damage);
            }
        }
    }
}