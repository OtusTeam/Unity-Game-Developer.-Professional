using System;
using System.Collections.Generic;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectEntity : MonoBehaviour, IEffectEntity
    {
        public event Action<IEffect> OnEffectAdded;
        
        public event Action<IEffect> OnEffectRemoved;
        
        [Inject]
        private IDynamicObject entity;

        private readonly HashSet<IEffect> activeEffects;

        public EffectEntity()
        {
            this.activeEffects = new HashSet<IEffect>();
        }

        public void AddEffect(IEffect effect)
        {
            if (this.activeEffects.Add(effect))
            {
                effect.OnDeactivated += this.OnEffectDeactivated;
                this.OnEffectAdded?.Invoke(effect);
            }
            
            effect.Activate(this.entity);
        }

        public IEnumerable<IEffect> GetActiveEffects()
        {
            return this.activeEffects;
        }

        public void RemoveEffect(IEffect effect)
        {
            if (this.activeEffects.Remove(effect))
            {
                effect.OnDeactivated -= this.OnEffectDeactivated;
                this.OnEffectRemoved?.Invoke(effect);
                effect.Deactivate(this.entity);
            }
        }

        private void OnEffectDeactivated(IEffect effect, IDynamicObject target)
        {
            if (this.entity != target)
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