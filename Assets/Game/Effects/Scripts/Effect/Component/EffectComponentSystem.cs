using System;
using System.Collections.Generic;
using DynamicObjects;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectComponentSystem : Effect
    {
        public override event Action<IEffect, IDynamicObject> OnActivated;
        
        public override event Action<IEffect, IDynamicObject> OnDeactivated;

        private readonly Dictionary<IDynamicObject, EffectComponent> activeEffectMap;

        [Inject]
        private EffectComponentPool componentPool;

        public EffectComponentSystem()
        {
            this.activeEffectMap = new Dictionary<IDynamicObject, EffectComponent>();
        }

        public override void Activate(IDynamicObject target)
        {
            if (!this.activeEffectMap.TryGetValue(target, out var effect))
            {
                effect = this.componentPool.Spawn();
                this.activeEffectMap.Add(target, effect);
            }

            effect.Activate(target);
            this.OnActivated?.Invoke(this, target);
        }

        public override void Deactivate(IDynamicObject target)
        {
            if (!this.activeEffectMap.TryGetValue(target, out var effect))
            {
                return;
            }

            effect.Deactivate(target);
            this.activeEffectMap.Remove(target);
            this.OnDeactivated?.Invoke(this, target);
        }
    }
}