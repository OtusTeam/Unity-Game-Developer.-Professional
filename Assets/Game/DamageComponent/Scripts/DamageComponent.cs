using UnityEngine;

namespace Otus
{
    public abstract class DamageComponent : MonoBehaviour
    {
        public abstract void TakeDamage(int damage);
    }
}