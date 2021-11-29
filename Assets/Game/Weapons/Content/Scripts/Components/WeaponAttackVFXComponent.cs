using System;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponAttackVFXComponent : WeaponAttackComponent
    {
        [SerializeField]
        private ParticleSystem[] vfxArray;

        public override event Action OnAttack;

        protected override void ProcessAttack()
        {
            foreach (var vfx in this.vfxArray)
            {
                vfx.Play(withChildren: true);
            }
            
            this.OnAttack?.Invoke();
        }
    }
}