using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectParticleShowComponent : EffectComponent
    {
        [SerializeField]
        private float vfxDuration;

        [SerializeField]
        private ParticleSystem vfxPrefab;
        
        public override void Activate(IDynamicObject target, IHandler handler)
        {
            var targetTransform = target.GetProperty<Transform>(PropertyKey.ROOT);
            var vfx = Instantiate(this.vfxPrefab, targetTransform); //Можно пул сделать
            Destroy(vfx.gameObject, this.vfxDuration);
        }

        public override void Deactivate()
        {
        }
    }
}