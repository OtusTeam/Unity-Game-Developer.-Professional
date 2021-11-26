using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponAttackCountdown : WeaponAttackComponent
    {
        public override event Action OnAttack;

        [SerializeField]
        private float countdown;

        [ReadOnly]
        [SerializeField]
        private float currentTime;

        private float fixedDeltaTime;
        
        protected override void ProcessAttack()
        {
            this.currentTime += this.countdown;
            this.OnAttack?.Invoke();
        }
        
        public override bool CanAttack()
        {
            return this.currentTime <= 0;
        }

        #region Lifecycle

        private void Awake()
        {
            this.fixedDeltaTime = Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            if (this.currentTime > 0)
            {
                this.currentTime -= this.fixedDeltaTime;
            }
        }

        #endregion
    }
}