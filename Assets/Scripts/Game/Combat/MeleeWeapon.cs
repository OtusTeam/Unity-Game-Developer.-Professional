using Foundation;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName="OTUS/Weapon/Melee")]
    public sealed class MeleeWeapon : AbstractWeapon
    {
        public float Damage;

        public override bool PrepareShoot(IInventoryStorage inventory, IWeaponAttack attack)
        {
            if (attack is IMeleeWeaponAttack meleeAttack)
                meleeAttack.BeginMeleeAttack(Damage);
            else
                DebugOnly.Error("Using melee weapon with wrong attack.");

            return true;
        }
    }
}
