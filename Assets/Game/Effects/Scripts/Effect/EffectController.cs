using System;
using System.Collections.Generic;
using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectController : Effect
    {
        public override event Action<IEffect, IDynamicObject> OnActivated;
        
        public override event Action<IEffect, IDynamicObject> OnDeactivated;

        private readonly Dictionary<IDynamicObject, IEffect> activeEffects;

        public EffectController()
        {
            this.activeEffects = new Dictionary<IDynamicObject, IEffect>();
        }

        public override void Activate(IDynamicObject target)
        {
            // this.activeEffects.Add(target);
        }

        public override void Deactivate(IDynamicObject target)
        {
            // this.activeEffects.Remove(target);
        }
    }
}