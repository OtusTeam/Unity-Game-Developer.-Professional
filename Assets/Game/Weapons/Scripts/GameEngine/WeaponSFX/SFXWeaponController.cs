using System;
using Otus;
using UnityEngine;

namespace Weapons.SFX
{
    public sealed class SFXWeaponController : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private AudioClip clip;
        
        protected override void ProcessAttack()
        {
            SoundManager.PlaySound(this.clip);
            this.OnAttack?.Invoke(this);
        }
    }
}