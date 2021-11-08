using System;
using Otus;
using UnityEngine;

namespace Weapons.InventoryWeapon
{
    public sealed class InventoryWeaponController : WeaponComponent
    {
        public override event Action<IWeapon> OnAttack;

        [SerializeField]
        private bool isActive;

        [SerializeField]
        private Inventory inventory;

        public override void Attack()
        {
            if (!this.isActive)
            {
                return;
            }
            
            if (this.inventory.HasShells())
            {
                this.inventory.SpendShell();
            }
        }

        public override bool CanAttack()
        {
            return this.inventory.HasShells();
        }

        public override void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }
}