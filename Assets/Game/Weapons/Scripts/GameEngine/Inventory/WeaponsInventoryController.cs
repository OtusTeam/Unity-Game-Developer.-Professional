using Otus.GameInventory;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponsInventoryController : MonoBehaviour
    {
        [Inject]
        private IInventoryItemManager inventoryItemManager;

        [Inject]
        private IWeaponsPool weaponsPool;
        
        [Inject]
        private IGameManager gameManager;

        #region Lifecycle

        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
            this.gameManager.OnFinishGame += this.OnFinishGame;
        }

        private void OnStartGame()
        {
            this.inventoryItemManager.OnItemAdded += this.OnItemAdded;
            this.inventoryItemManager.OnItemRemoved += this.OnItemRemoved;
        }

        private void OnFinishGame()
        {
            this.inventoryItemManager.OnItemAdded -= this.OnItemAdded;
            this.inventoryItemManager.OnItemRemoved -= this.OnItemRemoved;
        }

        private void OnDisable()
        {
            this.gameManager.OnStartGame -= this.OnStartGame;
            this.gameManager.OnFinishGame -= this.OnFinishGame;
        }

        #endregion
        
        private void OnItemAdded(Item item)
        {
            if (item.TryGetComponent(out WeaponInventoryComponent component))
            {
                this.weaponsPool.AddWeapon(component.Config);
            }
        }

        private void OnItemRemoved(Item item)
        {
            if (item.TryGetComponent(out WeaponInventoryComponent component))
            {
                this.weaponsPool.RemoveWeapon(component.Config.id);
            }
        }
    }
}