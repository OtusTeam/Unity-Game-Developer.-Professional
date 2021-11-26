namespace Otus.InventoryModule
{
    public interface IInventoryManager
    {
        Item GetItem(int id);

        Item[] GetItems(ItemType typeMask);

        void AddItems(Item[] inventoryItems);

        void AddItem(Item item);
    }
}