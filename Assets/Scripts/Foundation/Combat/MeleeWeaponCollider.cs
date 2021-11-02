using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class MeleeWeaponCollider : AbstractWeaponAttack, IAttacker
    {
        public IPlayer Player => player;
        [InjectOptional] IPlayer player;

        HashSet<ICharacterHealth> damaged = new HashSet<ICharacterHealth>();
        bool inAttack;
        float damage;

        public override void BeginAttack(float damage)
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

            var health = other.GetComponentInParent<ICharacterHealth>();
            if (health != null && damaged.Add(health))
                health.Damage(this, damage);
        }
    }
}
