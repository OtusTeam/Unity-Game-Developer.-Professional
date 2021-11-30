using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public class EffectComponentGroup : EffectComponent
    {
        [SerializeField]
        private EffectComponent[] components;
        
        public override void Activate(IDynamicObject target, IHandler handler)
        {
            for (int i = 0, count = this.components.Length; i < count; i++)
            {
                var component = this.components[i];
                component.Activate(target, handler);
            }
        }

        public override void Deactivate()
        {
            for (int i = 0, count = this.components.Length; i < count; i++)
            {
                var component = this.components[i];
                component.Deactivate();
            }
        }
    }
}