using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public interface IDealDamageHandler
    {
        void HandleDamage(Collider target, int damage);
    }
    
    public sealed class DealDamageHandler : MonoBehaviour, IDealDamageHandler
    {
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private MonoDynamicObjectCondition condition;
        
        public void HandleDamage(Collider target, int damage)
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