using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponManager : MonoBehaviour, IWeaponManager
    {
        public event Action<IWeapon> OnCurrentWeaponChanged;

        public IWeapon CurrentWeapon
        {
            get { return this.currentWeapon; }
        }

        [SerializeField]
        private Weapon currentWeapon;

        [SerializeField]
        private WeaponInfo[] weaponInfos;

        private Dictionary<string, Weapon> weaponMap;

        public void SetupCurrentWeapon(string weaponId)
        {
            var previousWeapon = this.currentWeapon;
            if (previousWeapon != null)
            {
                previousWeapon.SetActive(false);
            }
            
            var weapon = this.weaponMap[weaponId];
            weapon.SetActive(true);
            this.currentWeapon = weapon;
        }

        public void ChangeCurrentWeapon(string weaponId)
        {
            var previousWeapon = this.currentWeapon;
            var nextWeapon = this.weaponMap[weaponId];
            if (nextWeapon == previousWeapon)
            {
                return;
            }

            if (previousWeapon != null)
            {
                previousWeapon.SetActive(false);
            }

            nextWeapon.SetActive(true);
            this.currentWeapon = nextWeapon;
            this.OnCurrentWeaponChanged?.Invoke(nextWeapon);
        }

        public IWeapon GetWeapon(string weaponId)
        {
            return this.weaponMap[weaponId];
        }

        public IWeapon[] GetAllWeapons()
        {
            var result = new IWeapon[this.weaponMap.Count];
            var index = 0;
            foreach (var weapon in this.weaponMap.Values)
            {
                result[index++] = weapon;
            }
            
            return result;
        }

        private void Awake()
        {
            this.InitializeWeapons();
            this.SetCurrentWeaponByDefault();
        }

        private void InitializeWeapons()
        {
            var count = this.weaponInfos.Length;

            var weaponMap = new Dictionary<string, Weapon>(count);
            for (var i = 0; i < count; i++)
            {
                var weaponInfo = this.weaponInfos[i];
                var weapon = weaponInfo.Weapon;
                weapon.SetActive(false);
                weaponMap.Add(weaponInfo.Id, weapon);
            }

            this.weaponMap = weaponMap;
        }

        private void SetCurrentWeaponByDefault()
        {
            if (this.weaponMap.Count > 0)
            {
                this.currentWeapon = this.weaponMap.Values.First();
                this.currentWeapon.SetActive(true);
            }
        }

        [Serializable]
        public struct WeaponInfo
        {
            public string Id
            {
                get { return this.config.id; }
            }

            public Weapon Weapon
            {
                get { return this.weapon; }
            }

            [SerializeField]
            private WeaponConfig config;

            [SerializeField]
            private Weapon weapon;
        }
    }
}