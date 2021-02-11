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
            attack.BeginAttack(Damage);
            return true;
        }
    }
}
