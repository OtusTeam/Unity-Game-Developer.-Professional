using System;
using UnityEngine;

namespace Otus
{
    public sealed class VFXWeaponController : Weapon
    {
        [SerializeField]
        private ParticleSystem[] vfxArray;

        public override event Action<Weapon> OnAttack;

        protected override void ProcessAttack()
        {
            foreach (var vfx in this.vfxArray)
            {
                vfx.Play(withChildren: true);
            }
            
            this.OnAttack?.Invoke(this);
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            if (!isActive)
            {
                foreach (var vfx in this.vfxArray)
                {
                    vfx.Stop(withChildren: true);
                } 
            }
        }
    }
}