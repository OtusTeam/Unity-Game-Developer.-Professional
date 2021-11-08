using UnityEngine;

namespace Otus
{
    public sealed class DamageComponent : MonoBehaviour
    {
        public void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
        }

        [SerializeField]
        private int hitPoints;
    }
}