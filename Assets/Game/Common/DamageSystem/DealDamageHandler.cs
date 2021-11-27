using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class DealDamageHandler : MonoBehaviour
    {
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private MonoDynamicObjectCondition condition;
        
        public void HandleDamage(Collider target, int damage)
        {
            if (!target.TryGetComponent(out MonoDynamicObject dynamicObject))
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