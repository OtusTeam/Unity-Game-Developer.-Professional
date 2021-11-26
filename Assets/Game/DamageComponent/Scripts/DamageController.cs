using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class DamageController : MonoBehaviour
    {
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private DamageConditionChecker conditionProvider;
        
        public void HandleDamage(Collider target, int damage)
        {
            if (!target.TryGetComponent(out DamageComponent damageComponent))
            {
                return;
            }

            if (!this.hasCondition)
            {
                damageComponent.TakeDamage(damage);
                return;
            }
            
            if (this.conditionProvider.CanTakeDamage(damageComponent))
            {
                damageComponent.TakeDamage(damage);
            }
        }
    }
}