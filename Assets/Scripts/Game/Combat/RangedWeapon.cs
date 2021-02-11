using Foundation;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName="OTUS/Weapon/Ranged")]
    public class RangedWeapon : AbstractWeapon
    {
        public AbstractInventoryItem AmmoItem;
        public float Damage;

        public override bool CanShoot(IInventoryStorage inventory)
        {
            if (AmmoItem == null)
                return true;

            if (inventory == null)
                return false;

            return inventory.CountOf(AmmoItem) > 0;
        }

        public override bool PrepareShoot(IInventoryStorage inventory, IWeaponAttack attack)
        {
            if (AmmoItem != null && (inventory == null || !inventory.Remove(AmmoItem, 1)))
                return false;

            attack.BeginAttack(Damage);
            return true;
        }
    }
}
