using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class MeleeWeaponAttack : AbstractWeaponAttack, IAttacker, IMeleeWeaponAttack
    {
        [InjectOptional] IPlayer player = default;
        public IPlayer Player => player;

        HashSet<ICharacterHealth> damaged = new HashSet<ICharacterHealth>();
        bool inAttack;
        float damage;

        public void BeginMeleeAttack(float damage)
        {
            DebugOnly.Check(!inAttack, "BeginAttack called twice.");
            inAttack = true;
            this.damage = damage;
        }

        public override void EndAttack()
        {
            DebugOnly.Check(inAttack, "EndAttack called without attack.");
            inAttack = false;
            damaged.Clear();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!inAttack)
                return;

            var context = other.GetComponentInParent<Context>();
            if (context != null) {
                var health = context.Container.TryResolve<ICharacterHealth>();
                if (health != null && damaged.Add(health))
                    health.Damage(this, damage);
            }
        }
    }
}