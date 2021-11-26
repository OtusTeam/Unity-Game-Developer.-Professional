namespace Otus.InventoryModule
{
    public interface IItemComponent
    {
        ItemType Type { get; }

        IItemComponent Clone();
    }
}