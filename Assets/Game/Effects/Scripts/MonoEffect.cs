using System;
using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public abstract class MonoEffect : MonoBehaviour, IEffect
    {
        public event Action<IEffect, IDynamicObject> OnActivated;
        public event Action<IEffect, IDynamicObject> OnDeactivated;
        public abstract void Activate(IDynamicObject target);
        public void Deactivate(IDynamicObject target)
        {
            
        }

        public abstract void Cancel();
    }
}