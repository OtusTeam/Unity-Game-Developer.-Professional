using System;

namespace Otus.GameInventory
{
    [Flags]
    public enum ItemType
    {
        None = 0,
        Equipable = 1,
        Consumable = 2,
        Stackable = 4
    }
}