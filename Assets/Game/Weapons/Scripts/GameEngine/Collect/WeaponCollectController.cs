using System.Linq;
using DynamicObjects;
using Otus.GameInventory;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponCollectController : MonoBehaviour
    {
        [SerializeField]
        private new ObservableCollider collider;

        [Inject]
        private IInventoryItemManager inventoryManager;
        
        private void OnEnable()
        {
            this.collider.OnTriggerEntered += this.OnTriggerEntered;
        }

        private void OnDisable()
        {
            this.collider.OnTriggerEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            this.CollectWeapon(collider);
        }

        private void CollectWeapon(Collider collider)
        {
            if (!collider.TryGetComponent(out IDynamicObject dynamicObject))
            {
                return;
            }

            if (!dynamicObject.ContainsProperty(PropertyKey.INVENTORY_ITEM))
            {
                return;
            }

            var collectingItem = dynamicObject.GetProperty<Item>(PropertyKey.INVENTORY_ITEM);
            if (!collectingItem.TryGetComponent(out WeaponInventoryComponent inventoryComponent))
            {
                return;
            }

            var collectingWeaponId = inventoryComponent.Config.id;
            var allItems = this.inventoryManager.GetAllItems();
            
            if (allItems.Any(it => it.TryGetComponent(out WeaponInventoryComponent component) &&
                                   component.Config.id == collectingWeaponId))
            {
                return;
            }

            this.inventoryManager.AddItem(collectingItem);
            dynamicObject.InvokeMethod(ActionKey.COLLECT);
        }
    }
}