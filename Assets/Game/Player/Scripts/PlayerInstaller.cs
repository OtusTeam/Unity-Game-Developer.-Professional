using DynamicObjects;
using Otus.GameEffects;
using Otus.GameInventory;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoDynamicObject player;

        [Header("Move")]
        [SerializeField]
        private EntityMoveController moveController;
        
        [Header("Weapon")]
        [SerializeField]
        private WeaponCurrentManager weaponCurrentManager;

        [SerializeField]
        private WeaponsPool weaponsPool;
        
        [Header("Inventory")]
        [SerializeField]
        private InventoryItemManager inventoryItemManager;

        [Header("Effects")]
        [SerializeField]
        private EntityEffectSingleManager effectManager;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.player);

            this.Container.Bind<EntityMoveController>().FromInstance(this.moveController);
                
            this.Container.Bind<IWeaponCurrentManager>().FromInstance(this.weaponCurrentManager);
            this.Container.Bind<IWeaponsPool>().FromInstance(this.weaponsPool);
            
            this.Container.Bind<IInventoryItemManager>().FromInstance(this.inventoryItemManager);
            this.Container.Bind<IEntityEffectManager>().FromInstance(this.effectManager);
        }
    }
}