using System;
using DynamicObjects;

namespace Otus.GameEffects
{
    public interface IEffect
    {
        event Action<IEffect, IDynamicObject> OnActivated;

        event Action<IEffect, IDynamicObject> OnDeactivated;
        
        void Activate(IDynamicObject target);

        void Deactivate(IDynamicObject target);
    }
}