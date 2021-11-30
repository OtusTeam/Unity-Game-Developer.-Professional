using System.Collections;
using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectDealDamageCountdownComponent : EffectCoroutineComponent
    {
        [SerializeField]
        private float duration;

        [SerializeField]
        private float period;

        [SerializeField]
        private int damage;
        
        [ReadOnly]
        [SerializeField]
        private float remainingSeconds;

        protected override IEnumerator ProcessEffect(IDynamicObject target)
        {
            this.remainingSeconds = this.duration;
            while (this.remainingSeconds > 0)
            {
                target.TryInvokeMethod(ActionKey.TAKE_DAMAGE, this.damage);
                
                yield return new WaitForSeconds(this.period);
                this.remainingSeconds -= this.period;
            }
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            this.period = Mathf.Max(this.period, 0.01f);
        }
#endif
    }
}