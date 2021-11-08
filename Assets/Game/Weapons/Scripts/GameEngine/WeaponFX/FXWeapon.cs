using System;
using Otus;
using UnityEngine;

namespace Weapons.SFX
{
    public sealed class FXWeapon : Weapon
    {
        public override event Action<IWeapon> OnAttack;

        [SerializeField]
        private bool isActive;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private ParticleSystem vfx;

        public override void Attack()
        {
            if (this.isActive)
            {
                this.audioSource.Play();
                this.vfx.Play();
            }
        }

        public override bool CanAttack()
        {
            return this.isActive;
        }

        public override void SetActive(bool isActive)
        {
            this.isActive = isActive;
            this.audioSource.enabled = isActive;
        }
    }
}