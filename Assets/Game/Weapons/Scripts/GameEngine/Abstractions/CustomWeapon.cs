using System;
using UnityEngine;

namespace Otus
{
    public sealed class CustomWeapon : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [Space]
        [SerializeField]
        private Parameters parameters;

        protected override void ProcessAttack()
        {
            this.parameters.trigger.Attack();
        }

        public override bool CanAttack()
        {
            if (!base.CanAttack())
            {
                return false;
            }
            
            if (!this.parameters.trigger.CanAttack())
            {
                return false;
            }
            
            var weaponComponents = this.parameters.slaveComponents;
            for (int i = 0, count = weaponComponents.Length; i < count; i++)
            {
                var component = weaponComponents[i];
                if (!component.CanAttack())
                {
                    return false;
                }
            }

            return true;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            this.parameters.trigger.SetActive(isActive);
            
            var weaponComponents = this.parameters.slaveComponents;
            for (int i = 0, count = weaponComponents.Length; i < count; i++)
            {
                var component = weaponComponents[i];
                component.SetActive(isActive);
            }
        }

        private void OnTriggerAttack(Weapon _)
        {
            var weaponComponents = this.parameters.slaveComponents;
            for (int i = 0, count = weaponComponents.Length; i < count; i++)
            {
                var component = weaponComponents[i];
                component.Attack();
            }

            this.OnAttack?.Invoke(this);
        }

        private void OnEnable()
        {
            this.parameters.trigger.OnAttack += this.OnTriggerAttack;
        }

        private void OnDisable()
        {
            this.parameters.trigger.OnAttack -= this.OnTriggerAttack;
        }


        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public Weapon trigger;

            [SerializeField]
            public Weapon[] slaveComponents;
        }
    }
}