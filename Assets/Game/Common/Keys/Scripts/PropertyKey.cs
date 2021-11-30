using System;

namespace DynamicObjects
{
    [Serializable]
    public enum PropertyKey
    {
        ROOT = 1,
        PARENT = 2,
        ENTITY_ANIMATOR = 3,
        ENTITY_MESH = 4,
        
        
        
        INVENTORY_ITEM = 105,

        DEAL_DAMAGE_HANDLER = 10000,
        EFFECT_HANDLER = 10001
    }
}