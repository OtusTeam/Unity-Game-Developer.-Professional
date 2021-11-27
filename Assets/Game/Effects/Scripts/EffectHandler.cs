using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectHandler : MonoBehaviour
    { 
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private MonoDynamicObjectCondition condition;

        [SerializeField]
        private MonoEffect[] effects;

        public void HandleEffect(Collider target)
        {
            if (!target.TryGetComponent(out MonoDynamicObject dynamicObject))
            {
                return;
            }
            
            if (!this.hasCondition || this.condition.IsTrue(dynamicObject))
            {
                this.ApplyEffects(dynamicObject);
            }
        }

        private void ApplyEffects(MonoDynamicObject dynamicObject)
        {
            for (int i = 0, count = this.effects.Length; i < count; i++)
            {
                var effect = this.effects[i];
                dynamicObject.TryInvokeMethod(ActionKey.START_EFFECT, effect);
            }
        }
    }
}