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
        [SerializeField]
        private float remainingSeconds;
        
        private Coroutine takeDamageCoroutine;

        private IDynamicObject target;

        private IHandler targetHandler;
        
        public override void Activate(IDynamicObject target, IHandler handler)
        {
            this.ResetState();

            this.target = target;
            this.targetHandler = handler;
            this.takeDamageCoroutine = this.StartCoroutine(this.TakeDamageRoutine());
        }

        public override void Deactivate()
        {
            this.ResetState();
        }
        
        private IEnumerator TakeDamageRoutine()
        {
            this.remainingSeconds = this.config.duration;
            while (this.remainingSeconds > 0)
            {
                this.target.TryInvokeMethod(ActionKey.TAKE_DAMAGE, this.config.damage);
                yield return new WaitForSeconds(this.config.period);
                this.remainingSeconds -= this.config.period;
            }
            
            this.targetHandler.Deactivate(this.target);
        }
        
        private void ResetState()
        {
            this.targetHandler = null;
            this.target = null;
            
            if (this.takeDamageCoroutine != null)
            {
                this.StopCoroutine(this.takeDamageCoroutine);
                this.takeDamageCoroutine = null;
            }
        }
    }
}