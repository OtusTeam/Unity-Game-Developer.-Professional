using System.Collections.Generic;
using UnityEngine;

namespace Otus.InventoryModule
{
    public sealed class InventoryManager : MonoBehaviour, IInventoryManager
    {
        [SerializeField]
        private List<Item> items;

        public void AddItem(Item item)
        {
            this.items.Add(item);
        }

        public void AddItems(Item[] items)
        {
            this.items.AddRange(items);
        }

        public Item GetItem(int id)
        {
            return null;
        }

        public Item[] GetItems(ItemType typeMask)
        {
            var count = this.items.Count;
            var result = new Item[count];
            for (var i = 0; i < count; i++)
            {
                var item = items[i];
                if ((item.TypeMask & typeMask) == typeMask)
                {
                    result[i] = item;
                }
            }

            return result;
        }
    }
}


