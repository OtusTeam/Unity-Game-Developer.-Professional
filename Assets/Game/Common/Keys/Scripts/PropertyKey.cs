using System;

namespace DynamicObjects
{
    [Serializable]
    public enum PropertyKey
    {
        ROOT = 1,
        PARENT = 2,
        ANIMATOR = 3,
        
        INVENTORY_ITEM = 105,

        DEAL_DAMAGE_HANDLER = 10000,
        EFFECT_HANDLER = 10001
    }
}