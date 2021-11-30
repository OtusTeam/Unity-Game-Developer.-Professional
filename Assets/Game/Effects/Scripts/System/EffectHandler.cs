using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus.GameEffects
{
    public interface IEffectHandler
    {
        void HandleEffect(Collider target, IEffect effect);
    }
    
    public sealed class EffectHandler : MonoBehaviour, IEffectHandler
    { 
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private CompareTagCondition condition;

        public void HandleEffect(Collider target, IEffect effect)
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