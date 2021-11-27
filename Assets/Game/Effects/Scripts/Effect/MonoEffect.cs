using System;
using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public abstract class MonoEffect : MonoBehaviour, IEffect
    {
        public abstract event Action<IEffect, IDynamicObject> OnActivated;

        public abstract event Action<IEffect, IDynamicObject> OnDeactivated;
        
        public abstract void Activate(IDynamicObject target);
        
        public abstract void Deactivate(IDynamicObject target);
    }
}