using System;
using System.Collections.Generic;
using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Otus
{
    public interface IWeaponsPool
    {
        event Action<Weapon> OnWeaponAdded;

        event Action<Weapon> OnWeaponRemoved;

        int Count { get; }

        List<Weapon> GetAllWeapons();

        Weapon GetWeapon(string id);

        bool ContainsWeapon(string id);

        void AddWeapon(WeaponConfig config);

        void RemoveWeapon(string id);
    }

    public sealed class WeaponsPool : SerializedMonoBehaviour, IWeaponsPool
    {
        public event Action<Weapon> OnWeaponAdded;

        public event Action<Weapon> OnWeaponRemoved;

        public int Count
        {
            get { return this.weaponMap.Count; }
        }

        [ReadOnly]
        [ShowInInspector]
        private readonly Dictionary<string, Weapon> weaponMap;

        private readonly List<Weapon> weaponList;

        [Inject]
        private DiContainer di;

        [SerializeField]
        private Parameters parameters;

        public WeaponsPool()
        {
            this.weaponMap = new Dictionary<string, Weapon>();
            this.weaponList = new List<Weapon>();
        }

        public List<Weapon> GetAllWeapons()
        {
            return this.weaponList;
        }

        public Weapon GetWeapon(string id)
        {
            return this.weaponMap[id];
        }

        public bool ContainsWeapon(string id)
        {
            return this.weaponMap.ContainsKey(id);
        }

        public void AddWeapon(WeaponConfig config)
        {
            var weaponContainer = this.parameters.container;
            var weaponGO = this.di.InstantiatePrefabForComponent<MonoDynamicObject>(config.prefab, weaponContainer);
            
            var weapon = new Weapon(config, weaponGO);
            this.weaponList.Add(weapon);
            this.weaponMap.Add(config.id, weapon);
            
            this.OnWeaponAdded?.Invoke(weapon);
        }

        public void RemoveWeapon(string weaponId)
        {
            if (!this.weaponMap.TryGetValue(weaponId, out var weapon))
            {
                return;
            }

            this.weaponList.Remove(weapon);
            this.weaponMap.Remove(weaponId);
            
            this.OnWeaponRemoved?.Invoke(weapon);
            Destroy(weapon.DynamicObject);
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public Transform container;
        }
    }
}