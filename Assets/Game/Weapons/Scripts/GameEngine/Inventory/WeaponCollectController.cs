using DynamicObjects;
using Otus.InventoryModule;
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
            if (!collectingItem.TryGetComponent(out WeaponInventoryComponent _))
            {
                return;
            }

            if (this.inventoryManager.ContainsItem(collectingItem))
            {
                return;
            }

            this.inventoryManager.AddItem(collectingItem);
            dynamicObject.InvokeMethod(ActionKey.COLLECT);
        }
    }
}