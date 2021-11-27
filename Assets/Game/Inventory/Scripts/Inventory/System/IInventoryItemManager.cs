using System;

namespace Otus.GameInventory
{
    public interface IInventoryItemManager
    {
        event Action<Item> OnItemAdded;

        event Action<Item> OnItemRemoved;

        Item[] GetItems(ItemType typeMask);

        Item[] GetItems(Type componentType);

        Item[] GetAllItems();
        
        bool ContainsItem(Item item);
        
        void AddItem(Item item);

        void RemoveItem(Item item);
    }
}