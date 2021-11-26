using System;
using DynamicObjects;
using Otus;
using UnityEngine;
using Zenject;

namespace Weapons
{
    public sealed class WeaponAttackBulletComponent : WeaponAttackComponent, IBulletListener
    {
        public override event Action OnAttack;

        [SerializeField]
        private BulletConfig config;

        [SerializeField]
        private Transform firePoint;

        [Inject]
        private IDynamicObject weapon;

        [Inject]
        private IBulletManager bulletManager;

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
            this.weapon
                .GetProperty<IDynamicObject>(PropertyKey.PARENT)
                .GetProperty<IDamageHandler>(PropertyKey.DAMAGE_HANDLER)
                .HandleDamage(target, this.config.damage);
        }
    }
}