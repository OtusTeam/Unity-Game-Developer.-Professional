using System;
using UnityEngine;

namespace Otus
{
    public sealed class RayWeaponController : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private int damage;

        [Space]
        [SerializeField]
        private bool isActive;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private float maxDistance;
        
        public override void Attack()
        {
            if (!this.isActive)
            {
               return;
            }

            this.OnAttack?.Invoke(this);

            var ray = new Ray(this.firePoint.position, this.firePoint.forward);
            if (!Physics.Raycast(ray, out var hit, this.maxDistance))
            {
                return;
            }

            if (hit.transform.TryGetComponent(out DamageComponent eneny))
            {
                eneny.TakeDamage(this.damage);
            }
        }

        public override bool CanAttack()
        {
            return this.isActive;
        }

        public override void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }
}