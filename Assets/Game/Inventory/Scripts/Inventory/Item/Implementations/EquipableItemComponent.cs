using System;
using UnityEngine;

namespace Otus.InventoryModule
{
    [Serializable]
    public sealed class EquipableItemComponent : IItemComponent
    {
        public event Action<bool> OnItemEquiped;

        public ItemType Type
        {
            get { return this.type; }
        }

        public bool IsEquipped
        {
            get { return this.isEquiped; }
        }

        [SerializeField]
        private ItemType type;

        [SerializeField]
        private EquipSlot equipSlot;

        [SerializeField]
        private bool isEquiped;

        public void Equip(EquipSlot equipSlot, bool isEquip)
        {
            if ((this.equipSlot | equipSlot) == this.equipSlot)
            {
                this.isEquiped = isEquip;
                this.OnItemEquiped?.Invoke(isEquip);
            }
        }

        IItemComponent IItemComponent.Clone()
        {
            return new EquipableItemComponent
            {
                equipSlot = this.equipSlot,
                isEquiped = this.isEquiped
            };
        }
    }
}