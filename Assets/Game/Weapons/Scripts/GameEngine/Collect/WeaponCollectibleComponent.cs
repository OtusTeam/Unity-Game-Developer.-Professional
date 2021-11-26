using DynamicObjects;
using Otus.InventoryModule;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponCollectibleComponent : SerializedMonoBehaviour, IMethodDelegate
    {
        [SerializeField]
        private MonoDynamicObject weapon;

        [SerializeField]
        private ItemConfig itemConfig;
        
        private void Awake()
        {
            var inventoryItem = this.itemConfig.GetItem();
            this.weapon.AddProperty(PropertyKey.INVENTORY_ITEM, new PropertyProvider(inventoryItem));
            this.weapon.AddMethod(ActionKey.COLLECT, this);
        }

        object IMethodDelegate.Invoke(Args args)
        {
            Destroy(this.gameObject);
            return null;
        }
    }
}