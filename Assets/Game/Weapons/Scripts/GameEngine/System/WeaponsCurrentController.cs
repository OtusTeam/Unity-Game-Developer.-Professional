using System;
using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponsCurrentController : MonoBehaviour
    {
        public event Action<IDynamicObject> OnWeaponChanged;
        
        private IDynamicObject currentWeapon;

        [SerializeField]
        private Parameters parameters;

        private PropertyProvider parentProvider;
        
        public void SetupWeapon(IDynamicObject weapon)
        {
            weapon.AddProperty(PropertyKey.PARENT, this.parentProvider);
            weapon.InvokeMethod(ActionKey.SHOW);
            this.currentWeapon = weapon;
        }

        public void ChangeWeapon(IDynamicObject weapon)
        {
            var previousWeapon = this.currentWeapon;
            if (weapon == previousWeapon)
            {
                return;
            }

            if (previousWeapon != null)
            {
                previousWeapon.InvokeMethod(ActionKey.HIDE);
                previousWeapon.RemoveProperty(PropertyKey.PARENT);
            }

            this.SetupWeapon(weapon);
            this.OnWeaponChanged?.Invoke(weapon);
        }

        public bool TryGetWeapon(out IDynamicObject weapon)
        {
            if (this.currentWeapon != null)
            {
                weapon = this.currentWeapon;
                return true;
            }

            weapon = default;
            return false;
        }

        private void Awake()
        {
            this.parentProvider = new PropertyProvider(this.parameters.parent);

            if (this.parameters.hasInitialWeapon)
            {
                this.SetupWeapon(this.parameters.initialWeapon);
            }
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public MonoDynamicObject parent;

            [SerializeField]
            public bool hasInitialWeapon;
            
            [ShowIf("hasInitialWeapon")]
            [SerializeField]
            public MonoDynamicObject initialWeapon;
        }
    }
}