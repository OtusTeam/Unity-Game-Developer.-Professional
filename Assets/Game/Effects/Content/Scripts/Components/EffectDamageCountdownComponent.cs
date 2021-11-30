using System.Collections;
using DynamicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectDamageCountdownComponent : EffectComponent
    {
        [SerializeField]
        private EffectDamageCountdownConfig config;

        [ReadOnly]
        [ShowInInspector]
        private float remainingSeconds;

        private Coroutine takeDamageCoroutine;

        private IDynamicObject target;

        public override void Activate(IDynamicObject target)
        {
            this.ResetState();

            this.target = target;
            this.remainingSeconds = this.config.duration;
            this.takeDamageCoroutine = this.StartCoroutine(this.TakeDamageRoutine());
        }

        private IEnumerator TakeDamageRoutine()
        {
            while (this.remainingSeconds > 0)
            {
                this.target.TryInvokeMethod(ActionKey.TAKE_DAMAGE, this.config.damage);
                yield return new WaitForSeconds(this.config.period);
                this.remainingSeconds -= this.config.period;
            }
            
            this.ResetState();
        }

        public override void Deactivate(IDynamicObject target)
        {
            this.ResetState();
        }

        private void ResetState()
        {
            this.target = null;
            
            if (this.takeDamageCoroutine != null)
            {
                this.StopCoroutine(this.takeDamageCoroutine);
                this.takeDamageCoroutine = null;
            }
        }
    }
}