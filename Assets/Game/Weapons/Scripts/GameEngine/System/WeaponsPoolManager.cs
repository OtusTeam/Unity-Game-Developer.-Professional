using System;
using System.Collections.Generic;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public interface IWeaponsPool
    {
        event Action<Weapon> OnWeaponAdded;

        event Action<Weapon> OnWeaponRemoved;

        int Count { get; }

        IEnumerable<Weapon> GetAllWeapons();

        Weapon GetWeapon(string id);

        bool ContainsWeapon(string id);

        void AddWeapon(WeaponConfig config);

        void RemoveWeapon(string id);
    }

    public sealed class WeaponsPoolManager : MonoBehaviour, IWeaponsPool
    {
        public event Action<Weapon> OnWeaponAdded;

        public event Action<Weapon> OnWeaponRemoved;

        public int Count
        {
            get { return this.weaponMap.Count; }
        }

        private Dictionary<string, Weapon> weaponMap;

        [Inject]
        private DiContainer di;

        [Inject]
        private IGameManager gameManager;

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

            var weapon = new Weapon(config, weaponGO);
            this.weaponMap.Add(config.id, weapon);
            this.OnWeaponAdded?.Invoke(weapon);
        }

        public void RemoveWeapon(string weaponId)
        {
            if (!this.weaponMap.TryGetValue(weaponId, out var weapon))
            {
                return;
            }

            this.weaponMap.Remove(weaponId);
            this.OnWeaponRemoved?.Invoke(weapon);
            Destroy(weapon.DynamicObject);
        }

        #region Lifecycle

        private void Awake()
        {
            this.weaponMap = new Dictionary<string, Weapon>();
        }

        private void OnEnable()
        {
            this.gameManager.OnInitializeGame += this.OnGameInitialized;
        }

        private void OnGameInitialized()
        {
            this.InitializeWeapons();
        }

        private void OnDisable()
        {
            this.gameManager.OnInitializeGame -= this.OnGameInitialized;
        }

        #endregion

        private void InitializeWeapons()
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

        public MonoDynamicObject DynamicObject
        {
            get { return this.dynamicObject; }
        }

        [SerializeField]
        private WeaponConfig config;

        [SerializeField]
        private MonoDynamicObject dynamicObject;

        public Weapon(WeaponConfig config, MonoDynamicObject dynamicObject)
        {
            this.config = config;
            this.dynamicObject = dynamicObject;
        }
    }
}