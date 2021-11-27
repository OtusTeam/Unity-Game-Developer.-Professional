using System;
using DynamicObjects;

namespace Otus.GameEffects
{
    public class BurnEffect : MonoEffect
    {
        public override event Action<IEffect, IDynamicObject> OnActivated;
        
        public override event Action<IEffect, IDynamicObject> OnDeactivated;

        public override void Activate(IDynamicObject target)
        {
            
        }

        public override void Deactivate(IDynamicObject target)
        {
        }
    }
}