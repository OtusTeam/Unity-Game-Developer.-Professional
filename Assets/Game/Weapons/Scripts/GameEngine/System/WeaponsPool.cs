using System;
using System.Collections.Generic;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public interface IWeaponsPool
    {
        int Count { get; }

        IEnumerable<Weapon> GetAllWeapons();

        Weapon GetWeapon(string id);

        bool ContainsWeapon(string id);

        void AddWeapon(WeaponConfig config);

        void RemoveWeapon(string id);
    }

    public sealed class WeaponsPool : MonoBehaviour, IWeaponsPool
    {
        public int Count
        {
            get { return this.weaponMap.Count; }
        }

        private Dictionary<string, Weapon> weaponMap;
        
        [Inject]
        private DiContainer di;
        
        [SerializeField]
        private Parameters parameters;

        public IEnumerable<Weapon> GetAllWeapons()
        {
            foreach (var weapon in this.weaponMap)
            {
                yield return weapon.Value;
            }
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
            weaponGO.InvokeMethod(ActionKey.HIDE);
            
            var weapon = new Weapon(config, weaponGO);
            this.weaponMap.Add(config.id, weapon);
        }

        public void RemoveWeapon(string weaponId)
        {
            if (!this.weaponMap.TryGetValue(weaponId, out var weapon))
            {
                return;
            }

            this.weaponMap.Remove(weaponId);
            Destroy(weapon.GameObject);
        }
        
        private void Start()
        {
            this.weaponMap = new Dictionary<string, Weapon>();
            this.Initialize();
        }

        private void Initialize()
        {
            var weaponConfigs = this.parameters.initialWeapons;
            for (var i = 0; i < weaponConfigs.Length; i++)
            {
                var config = weaponConfigs[i];
                this.AddWeapon(config);
            }
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public Transform container;
            
            [SerializeField]
            public WeaponConfig[] initialWeapons;
        }
    }

    [Serializable]
    public sealed class Weapon
    {
        public WeaponConfig Config
        {
            get { return this.config; }
        }

        public MonoDynamicObject GameObject
        {
            get { return this.gameObject; }
        }

        [SerializeField]
        private WeaponConfig config;

        [SerializeField]
        private MonoDynamicObject gameObject;

        public Weapon(WeaponConfig config, MonoDynamicObject gameObject)
        {
            this.config = config;
            this.gameObject = gameObject;
        }
    }
}