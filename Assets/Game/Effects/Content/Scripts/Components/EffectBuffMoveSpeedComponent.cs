using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectBuffMoveSpeedComponent : EffectComponent
    {
        [SerializeField]
        private FloatMultiplier multiplier;
        
        private IDynamicObject target;
        
        public override void Activate(IDynamicObject target, IHandler handler)
        {
            if (this.target == target)
            {
                return;
            }
            
            target.TryInvokeMethod(ActionKey.ADD_MOVE_SPEED_MULTIPLIER, this.multiplier);
            this.target = target;
        }

        public override void Deactivate()
        {
            this.target.TryInvokeMethod(ActionKey.REMOVE_MOVE_SPEED_MULTIPLIER, this.multiplier);
            this.target = null;
        }
    }
}