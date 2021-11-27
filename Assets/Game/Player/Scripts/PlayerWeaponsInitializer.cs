using System;
using Otus.GameInventory;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerWeaponsInitializer : MonoBehaviour
    {
        [Inject]
        private IGameManager gameManager;

        [Inject]
        private IInventoryItemManager inventoryItemManager;

        [Inject]
        private IWeaponsPool weaponsPool;

        [Inject]
        private IWeaponCurrentManager weaponCurrentManager;

        [SerializeField]
        private WeaponData[] weaponDataSet;

        private void OnEnable()
        {
            this.gameManager.OnInitializeGame += this.OnInitializeGame;
        }

        private void OnDisable()
        {
            this.gameManager.OnInitializeGame -= this.OnInitializeGame;
        }

        private void OnInitializeGame()
        {
            this.InitializeWeapons(this.weaponDataSet);
        }

        public void InitializeWeapons(WeaponData[] dataSet)
        {
            foreach (var data in dataSet)
            {
                this.InitializeWeapon(data);
            }
        }

        private void InitializeWeapon(WeaponData data)
        {
            var inventoryItem = data.inventoryConfig.GetItem();
            this.inventoryItemManager.AddItem(inventoryItem);

            var inventoryComponent = inventoryItem.GetComponent<WeaponInventoryComponent>();
            var weaponConfig = inventoryComponent.Config;
            this.weaponsPool.AddWeapon(weaponConfig);

            if (data.isSelected)
            {
                var weapon = this.weaponsPool.GetWeapon(weaponConfig.id);
                this.weaponCurrentManager.SetupWeapon(weapon.DynamicObject);
            }
        }

        [Serializable]
        public struct WeaponData
        {
            [SerializeField]
            public bool isSelected;

            [SerializeField]
            public ItemConfig inventoryConfig;
        }
    }
}