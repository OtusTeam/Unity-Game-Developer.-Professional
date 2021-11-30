using System.Collections;
using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public abstract class EffectCoroutineComponent : EffectComponent
    {
        private Coroutine coroutine;

        private IDynamicObject target;

        private IHandler targetHandler;

        public sealed override void Activate(IDynamicObject target, IHandler handler)
        {
            this.ResetState();

            this.target = target;
            this.targetHandler = handler;
            this.OnActivate(target);

            this.coroutine = this.StartCoroutine(this.Routine());
        }

        public sealed override void Deactivate()
        {
            this.OnDeactivate(this.target);
            this.ResetState();
        }
        
        protected virtual void OnActivate(IDynamicObject target)
        {
        }

        protected virtual void OnDeactivate(IDynamicObject target)
        {
        }
        
        protected abstract IEnumerator ProcessEffect(IDynamicObject target);

        private IEnumerator Routine()
        {
            yield return this.ProcessEffect(this.target);
            this.targetHandler.Deactivate(this.target);
        }

        private void ResetState()
        {
            this.targetHandler = null;
            this.target = null;

            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }
    }
}