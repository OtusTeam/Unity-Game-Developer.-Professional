using System;
using Otus;
using UnityEngine;
using Zenject;

namespace Weapons.SFX
{
    public sealed class WeaponAttackSFXComponent : WeaponAttackComponent
    {
        public override event Action OnAttack;

        [SerializeField]
        private AudioClip clip;

        [Inject]
        private SoundManager soundManager;

        protected override void ProcessAttack()
        {
            this.soundManager.PlaySound(this.clip);
            this.OnAttack?.Invoke();
        }
    }
}