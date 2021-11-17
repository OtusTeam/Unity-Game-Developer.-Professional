using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class RangedWeaponAttack : AbstractWeaponAttack, IAttacker, IRangedWeaponAttack
    {
        [Inject] IShootingManager shootingManager = default;
        [InjectOptional] IPlayer player = default;
        public IPlayer Player => player;

        public LayerMask LayerMask;

        public void BeginRangedAttack(RangedWeaponParameters parameters, float damage)
        {
            shootingManager.Shoot(transform, this, parameters, LayerMask.value, damage);
        }

        public override void EndAttack()
        {
        }
    }
}
