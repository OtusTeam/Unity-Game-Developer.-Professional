using System;
using System.Collections.Generic;
using DynamicObjects;

namespace Otus.GameEffects
{
    public sealed class SingleEffectManager : IEffectManager
    {
        public event Action<IEffect> OnEffectAdded;
        
        public event Action<IEffect> OnEffectRemoved;
        
        private readonly IDynamicObject target;

        private readonly HashSet<IEffect> activeEffects;

        public SingleEffectManager(IDynamicObject target)
        {
            this.target = target;
            this.activeEffects = new HashSet<IEffect>();
        }
        
        public void AddEffect(IEffect effect)
        {
            if (this.activeEffects.Add(effect))
            {
                effect.OnDeactivated += this.OnEffectDeactivated;
                this.OnEffectAdded?.Invoke(effect);
            }
            
            effect.Activate(this.target);
        }

        public IEnumerable<IEffect> GetEffects()
        {
            return this.activeEffects;
        }

        public void RemoveEffect(IEffect effect)
        {
            if (this.activeEffects.Remove(effect))
            {
                effect.OnDeactivated -= this.OnEffectDeactivated;
                this.OnEffectRemoved?.Invoke(effect);
                effect.Deactivate(this.target);
            }
        }

        private void OnEffectDeactivated(IEffect effect, IDynamicObject target)
        {
            if (this.target != target)
            {
                return;
            }

            if (this.activeEffects.Remove(effect))
            {
                effect.OnDeactivated -= this.OnEffectDeactivated;
                this.OnEffectRemoved?.Invoke(effect);
            }
        }
    }
}