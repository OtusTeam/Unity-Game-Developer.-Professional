using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public abstract class EffectComponent : MonoBehaviour
    {
        public abstract void Activate(IDynamicObject target, IHandler handler);

        public abstract void Deactivate();
        
        public interface IHandler
        {
            void Deactivate(IDynamicObject target);
        }
    }
}