using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectVFXComponent : EffectComponent
    {
        [SerializeField]
        private float vfxEndTime = 2.5f;

        [SerializeField]
        private ParticleSystem vfxPrefab;

        private IDynamicObject currentTarget;

        private ParticleSystem currentVFX;

        public override void Activate(IDynamicObject target, IHandler handler)
        {
            if (this.currentTarget == target)
            {
                return;
            }
            
            this.currentTarget = target;
            
            var targetTransform = target.GetProperty<Transform>(PropertyKey.ROOT);
            this.currentVFX = Instantiate(this.vfxPrefab, targetTransform);
            this.currentVFX.Play(withChildren:true);
        }

        public override void Deactivate()
        {
            this.currentTarget = null;
            if (this.currentVFX != null)
            {
                var previousVFX = this.currentVFX;
                previousVFX.Stop(withChildren:true);
                Destroy(previousVFX.gameObject, this.vfxEndTime);
                this.currentVFX = null;                
            }
        }
    }
}