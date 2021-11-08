using UnityEngine;

namespace Otus
{
    public sealed class DamageHitPointsComponent : DamageComponent
    {
        [SerializeField]
        private int hitPoints;
        
        public override void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
        }
    }
}