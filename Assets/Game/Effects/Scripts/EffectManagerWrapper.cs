using System.Collections.Generic;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectManagerWrapper : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        private readonly List<IEffect> appliedEffects;

        public EffectManagerWrapper()
        {
            this.appliedEffects = new List<IEffect>();
        }

        private void Awake()
        {
            this.entity.AddMethod(ActionKey.APPLY_EFFECT, new MethodDelegate(this.ApplyEffect));
        }

        private object ApplyEffect(object data)
        {
            var effect = (IEffect) data;
            effect.OnDeactivated += this.OnEffectDeactivated;
            effect.Activate();
        }

        private void OnEffectDeactivated()
        {
            
        }
    }
}