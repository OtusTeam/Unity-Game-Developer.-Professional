using System;
using DynamicObjects;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponCurrentManager : MonoBehaviour
    {
        public delegate void WeaponSetupedDelegate(MonoDynamicObject weapon);

        public delegate void WeaponChangedDelegate(MonoDynamicObject previousWeapon, MonoDynamicObject nextWeapon);
        
        public event WeaponSetupedDelegate OnWeaponSetuped;
        
        public event WeaponChangedDelegate OnWeaponChanged;
        
        private MonoDynamicObject currentWeapon;

        [SerializeField]
        private Parameters parameters;

        private PropertyProvider parentProvider;
        
        public void SetupWeapon(MonoDynamicObject weapon)
        {
            this.SetWeapon(weapon);
            this.OnWeaponSetuped?.Invoke(weapon);
        }

        public void ChangeWeapon(MonoDynamicObject weapon)
        {
            var previousWeapon = this.currentWeapon;
            if (weapon == previousWeapon)
            {
                return;
            }

            if (previousWeapon != null)
            {
                previousWeapon.RemoveProperty(PropertyKey.PARENT);
            }

            this.SetWeapon(weapon);
            this.OnWeaponChanged?.Invoke(previousWeapon, weapon);
        }

        public bool TryGetWeapon(out MonoDynamicObject weapon)
        {
            if (this.currentWeapon != null)
            {
                weapon = this.currentWeapon;
                return true;
            }

            weapon = default;
            return false;
        }
        
        private void SetWeapon(MonoDynamicObject weapon)
        {
            weapon.AddProperty(PropertyKey.PARENT, this.parentProvider);
            this.currentWeapon = weapon;
        }

        private void Awake()
        {
            this.parentProvider = new PropertyProvider(this.parameters.parent);
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public MonoDynamicObject parent;
        }
    }
}