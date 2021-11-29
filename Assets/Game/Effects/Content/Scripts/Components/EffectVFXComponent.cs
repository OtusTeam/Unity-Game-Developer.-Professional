using DynamicObjects;
using UnityEngine;

namespace Otus.GameEffects
{
    public sealed class EffectVFXComponent : EffectComponent
    {
        [SerializeField]
        private Transform rootTransform;
        
        [SerializeField]
        private Transform vfxTransform;

        [SerializeField]
        private ParticleSystem[] particleSystems;
        
        public override void Activate(IDynamicObject target)
        {
            var targetTransform = target.GetProperty<Transform>(PropertyKey.ROOT);
            this.SetParent(targetTransform);
            this.StartVFXs();
        }

        public override void Deactivate(IDynamicObject target)
        {
            this.SetParent(this.rootTransform);
            this.StopVFXs();
        }
        
        private void SetParent(Transform parent)
        {
            this.vfxTransform.SetParent(parent);
            this.vfxTransform.position = Vector3.zero;
            this.vfxTransform.eulerAngles = Vector3.zero;
        }
        
        private void StartVFXs()
        {
            for (int i = 0, count = this.particleSystems.Length; i < count; i++)
            {
                var vfx = this.particleSystems[i];
                vfx.Play(withChildren:true);
            }
        }
        
        private void StopVFXs()
        {
            for (int i = 0, count = this.particleSystems.Length; i < count; i++)
            {
                var vfx = this.particleSystems[i];
                vfx.Play(withChildren:true);
            }
        }
    }
}