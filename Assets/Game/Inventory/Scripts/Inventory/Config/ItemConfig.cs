using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus.GameInventory
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public sealed class ItemConfig : SerializedScriptableObject
    {
        [SerializeField]
        private List<IItemComponent> components;

        public Item GetItem()
        {
            var count = this.components.Count;
            var components = new IItemComponent[count];
            for (var i = 0; i < count; i++)
            {
                var component = this.components[i];
                components[i] = component.Clone();
            }

            return new Item(components);
        }
    }
}