using System;
using DynamicObjects;
using Otus;
using Otus.GameEffects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Weapons
{
    public sealed class WeaponAttackBulletComponent : WeaponAttackComponent
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
        private EffectAsset effect;

        protected override void ProcessAttack()
        {
            this.bulletManager.LaunchBullet(
                position: this.firePoint.position,
                rotation: this.firePoint.rotation,
                direction: this.firePoint.forward,
                listener: new BulletHandler(this) //ALLOCATION WHEN FIRE
            );

            this.OnAttack?.Invoke();
        }
        
        //TODO: POOL
        private sealed class BulletHandler : IBulletListener
        {
            private readonly IDynamicObject parent;

            private readonly bool hasDamage;

            private readonly int damage;

            private readonly bool hasEffect;

            private readonly EffectAsset effect;

            public BulletHandler(WeaponAttackBulletComponent component)
            {
                this.parent = component.weapon.GetProperty<IDynamicObject>(PropertyKey.PARENT);

                this.hasDamage = component.hasDamage;
                this.damage = component.damage;

                this.hasEffect = component.hasEffect;
                this.effect = component.effect;
            }

            void IBulletListener.OnBulletCollided(Collider target)
            {
                if (this.hasDamage)
                {
                    this.parent
                        .GetProperty<IDamageDealHandler>(PropertyKey.DAMAGE_DEAL_HANDLER)
                        .DealDamage(target, this.damage);
                }
            
                if (this.hasEffect)
                {
                    this.parent
                        .GetProperty<IEffectApplyHandler>(PropertyKey.EFFECT_APPLY_HANDLER)
                        .ApplyEffect(target, this.effect);
                }
            }
        }
    }
}