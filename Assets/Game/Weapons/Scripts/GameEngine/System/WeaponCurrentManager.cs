using System;
using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public interface IWeaponCurrentManager
    {
        delegate void WeaponSetupedDelegate(MonoDynamicObject weapon);

        delegate void WeaponChangedDelegate(MonoDynamicObject previousWeapon, MonoDynamicObject nextWeapon);
        
        event WeaponSetupedDelegate OnWeaponSetuped;
        
        event WeaponChangedDelegate OnWeaponChanged;
        
        void SetupWeapon(MonoDynamicObject weapon);
        
        void ChangeWeapon(MonoDynamicObject weapon);
        
        bool TryGetWeapon(out MonoDynamicObject weapon);
    }

    public sealed class WeaponCurrentManager : MonoBehaviour, IWeaponCurrentManager
    {
        public event IWeaponCurrentManager.WeaponSetupedDelegate OnWeaponSetuped;
        
        public event IWeaponCurrentManager.WeaponChangedDelegate OnWeaponChanged;
        
        [ReadOnly]
        [ShowInInspector]
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