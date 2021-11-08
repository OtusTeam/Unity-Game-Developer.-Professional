using System;
using Otus;
using UnityEngine;

namespace Weapons.SFX
{
    public sealed class FXWeapon : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private ParticleSystem vfx;

        protected override void ProcessAttack()
        {
            this.audioSource.Play();
            this.vfx.Play();
        }
    }
}