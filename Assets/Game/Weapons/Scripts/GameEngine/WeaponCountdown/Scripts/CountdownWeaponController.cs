using System;
using UnityEngine;

namespace Otus
{
    public sealed class CountdownWeaponController : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private float countdown;

        private float currentTime;

        protected override void ProcessAttack()
        {
            this.currentTime += this.countdown;
        }

        public override bool CanAttack()
        {
            return this.currentTime <= 0;
        }

        public override void SetActive(bool isActive)
        {
            this.enabled = isActive;
            this.currentTime = 0;
        }

        private void FixedUpdate()
        {
            if (this.currentTime > 0)
            {
                this.currentTime -= Time.fixedDeltaTime;
            }
        }
    }
}