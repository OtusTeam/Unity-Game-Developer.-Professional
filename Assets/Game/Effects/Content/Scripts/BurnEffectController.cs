using System;
using System.Collections.Generic;
using DynamicObjects;

namespace Otus.GameEffects
{
    public sealed class BurnEffectController : Effect
    {
        public override event Action<IEffect, IDynamicObject> OnActivated;
        
        public override event Action<IEffect, IDynamicObject> OnDeactivated;

        private List<IDynamicObject> targets;
        
        public override void Activate(IDynamicObject target)
        {
            
        }

        public override void Deactivate(IDynamicObject target)
        {
        }
        
        //DISABLE_MOVE _EVENT
    }
}