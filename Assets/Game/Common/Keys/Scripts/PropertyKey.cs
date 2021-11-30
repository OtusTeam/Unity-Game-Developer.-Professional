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
        ACTIVE_EFFECTS = 106,

        DAMAGE_DEAL_HANDLER = 10000,
        EFFECT_APPLY_HANDLER = 10001
    }
}