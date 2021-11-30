using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public interface IDamageDealHandler
    {
        void DealDamage(Collider target, int damage);
    }
    
    public sealed class DamageDealHandler : MonoBehaviour, IDamageDealHandler
    {
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private MonoDynamicObjectCondition condition;
        
        public void DealDamage(Collider target, int damage)
        {
            if (!target.TryGetComponent(out IMonoDynamicObject dynamicObject))
            {
                return;
            }

            if (!this.hasCondition || this.condition.IsTrue(dynamicObject))
            {
                dynamicObject.TryInvokeMethod(ActionKey.TAKE_DAMAGE, damage);
            }
        }
    }
}