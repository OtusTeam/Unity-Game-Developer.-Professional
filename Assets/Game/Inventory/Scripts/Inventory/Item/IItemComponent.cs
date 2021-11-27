namespace Otus.GameInventory
{
    public interface IItemComponent
    {
        ItemType Type { get; }

        IItemComponent Clone();
    }
}