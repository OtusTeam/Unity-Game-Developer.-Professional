using System;
using Otus;
using UnityEngine;
using Zenject;

namespace Weapons
{
    public sealed class BulletWeaponController : Weapon, IBulletListener
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private BulletConfig config;

        [SerializeField]
        private Transform firePoint;

        [Inject] // Он один, поэтому делаем через Zenject...
        private IBulletManager bulletManager;

        [SerializeField] // Локальный контроллер.
        private DamageController damageController;
        
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
            this.damageController.HandleDamage(target, this.config.damage);
        }
    }
}