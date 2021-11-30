using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public interface IEffectApplyHandler
    {
        void ApplyEffect(Collider target, EffectAsset effect);
    }
    
    public sealed class EffectApplyHandler : MonoBehaviour, IEffectApplyHandler
    {
        [Inject]
        private EffectManager effectManager;
        
        [SerializeField]
        private bool hasCondition;
        
        [ShowIf("hasCondition")]
        [SerializeField]
        private CompareTagCondition condition;

        public void ApplyEffect(Collider target, EffectAsset effect)
        {
            if (!target.TryGetComponent(out IMonoDynamicObject dynamicObject))
            {
                return;
            }

            if (!this.hasCondition || this.condition.IsTrue(dynamicObject))
            {
                this.effectManager.ApplyEffect(effect, dynamicObject);
            }
        }
    }
}