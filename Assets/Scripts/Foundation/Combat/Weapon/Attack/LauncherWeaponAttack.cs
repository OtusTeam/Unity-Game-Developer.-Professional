using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class LauncherWeaponAttack : AbstractWeaponAttack, IAttacker, ILauncherWeaponAttack
    {
        [Inject(Id = nameof(LauncherWeapon))] MovingForwardProjectile.Factory factory = default;
        [InjectOptional] IPlayer player = default;
        public IPlayer Player => player;

        AbstractProjectile projectile;

        void Start()
        {
            CreateNewProjectile();
        }

        void CreateNewProjectile()
        {
            projectile = factory.Create(transform);
        }

        public void BeginLauncherAttack(float damage)
        {
            if (projectile == null) {
                DebugOnly.Error("BeginLauncherAttack: no projectile.");
                return;
            }

            projectile.Launch(damage);
            projectile = null;
        }

        public override void EndAttack()
        {
        }

        public override void EndCooldown()
        {
            CreateNewProjectile();
        }
    }
}
