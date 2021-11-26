using System;
using Otus;
using UnityEngine;
using Zenject;

namespace Weapons.SFX
{
    public sealed class SFXWeaponController : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private AudioClip clip;

        [Inject]
        private SoundManager soundManager;
        
        protected override void ProcessAttack()
        {
            this.soundManager.PlaySound(this.clip);
            this.OnAttack?.Invoke(this);
        }
    }
}