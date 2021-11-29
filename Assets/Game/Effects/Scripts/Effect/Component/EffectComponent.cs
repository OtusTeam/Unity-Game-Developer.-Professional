using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public abstract class EffectComponent : MonoBehaviour
    {
        public abstract void Activate(IDynamicObject target);

        public abstract void Deactivate(IDynamicObject target);
    }
}