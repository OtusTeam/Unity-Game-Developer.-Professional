using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus.GameEffects
{
    public interface IEffectApplyHandler
    {
        void ApplyEffect(Collider target, IEffect effect);
    }
    
    public sealed class EffectApplyHandler : MonoBehaviour, IEffectApplyHandler
    { 
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private CompareTagCondition condition;

        public void ApplyEffect(Collider target, IEffect effect)
        {
            if (!target.TryGetComponent(out IMonoDynamicObject dynamicObject))
            {
                return;
            }

            if (!this.hasCondition || this.condition.IsTrue(dynamicObject))
            {
                dynamicObject.TryInvokeMethod(ActionKey.START_EFFECT, effect);
            }
        }
    }
}