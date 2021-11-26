using UnityEngine;

namespace Otus
{
    //Condition Provider
    public abstract class DamageConditionChecker : MonoBehaviour
    {
        public abstract bool CanTakeDamage(DamageComponent damageComponent);
    }
}