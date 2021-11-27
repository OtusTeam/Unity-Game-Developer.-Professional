using DynamicObjects;
using Otus.GameInventory;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoDynamicObject player;

        [Header("Weapon")]
        [SerializeField]
        private WeaponCurrentManager weaponCurrentManager;

        [SerializeField]
        private WeaponsPool weaponsPool;

        [Header("Deal Damage")]
        [SerializeField]
        private DealDamageHandler dealDamageHandler;
        
        [Header("Inventory")]
        [SerializeField]
        private InventoryItemManager inventoryItemManager;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.player);

            this.Container.Bind<IWeaponCurrentManager>().FromInstance(this.weaponCurrentManager);
            this.Container.Bind<IWeaponsPool>().FromInstance(this.weaponsPool);
            
            this.Container.Bind<DealDamageHandler>().FromInstance(this.dealDamageHandler);

            this.Container.Bind<IInventoryItemManager>().FromInstance(this.inventoryItemManager);
        }
    }
}