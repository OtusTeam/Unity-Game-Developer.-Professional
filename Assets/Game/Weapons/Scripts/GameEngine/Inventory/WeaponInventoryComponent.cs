using System;
using Otus.InventoryModule;
using UnityEngine;

namespace Otus
{
    [Serializable]
    public sealed class WeaponInventoryComponent : IItemComponent
    {
        public ItemType Type
        {
            get { return ItemType.None; }
        }

        public WeaponConfig Config
        {
            get { return this.config; }
        }

        [SerializeField]
        private WeaponConfig config;

        public IItemComponent Clone()
        {
            return new WeaponInventoryComponent
            {
                config = this.config
            };
        }
    }
}