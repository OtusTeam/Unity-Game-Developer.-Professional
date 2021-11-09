using System;
using Otus;
using UnityEngine;

namespace Weapons.SFX
{
    public sealed class SFXWeaponController : Weapon, IPropertyInjector
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private AudioClip clip;

        private SoundManager soundManager;
        
        protected override void ProcessAttack()
        {
            this.soundManager.PlaySound(this.clip);
            this.OnAttack?.Invoke(this);
        }

        void IPropertyInjector.Set(IPropertyProvider provider)
        {
            this.soundManager = provider.Get<SoundManager>(PropertyId.SOUND_MANAGER);
        }
    }
}