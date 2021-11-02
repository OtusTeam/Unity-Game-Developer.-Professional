using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    public abstract class AbstractWeaponAttack : AbstractBehaviour, IWeaponAttack
    {
        public abstract void BeginAttack(float damage);
        public abstract void EndAttack();
    }
}
