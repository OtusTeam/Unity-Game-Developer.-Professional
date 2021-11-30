using System;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponAttackComponentGroup : WeaponAttackComponent
    {
        [SerializeField]
        private WeaponAttackComponent triggerComponent;

        [SerializeField]
        private WeaponAttackComponent[] slaveComponents;

        public override event Action OnAttack;

        public override bool CanAttack()
        {
            if (!this.triggerComponent.CanAttack())
            {
                return false;
            }

            for (int i = 0, count = this.slaveComponents.Length; i < count; i++)
            {
                var component = this.slaveComponents[i];
                if (!component.CanAttack())
                {
                    return false;
                }
            }

            return true;
        }

        protected override void ProcessAttack()
        {
            this.triggerComponent.Attack();
        }

        private void OnTriggerAttack()
        {
            for (int i = 0, count = this.slaveComponents.Length; i < count; i++)
            {
                var component = this.slaveComponents[i];
                component.Attack();
            }
            
            this.OnAttack?.Invoke();
        }

        #region Lifecycle

        private void OnEnable()
        {
            this.triggerComponent.OnAttack += this.OnTriggerAttack;
        }

        private void OnDisable()
        {
            this.triggerComponent.OnAttack -= this.OnTriggerAttack;
        }

        #endregion
    }
}