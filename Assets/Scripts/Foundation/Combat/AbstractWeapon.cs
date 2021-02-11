using UnityEngine;

namespace Foundation
{
    public abstract class AbstractWeapon : ScriptableObject
    {
        public AbstractInventoryItem InventoryItem;
        public AbstractInventoryItem AmmoItem;
        public float AttackCooldownTime;
    }
}
