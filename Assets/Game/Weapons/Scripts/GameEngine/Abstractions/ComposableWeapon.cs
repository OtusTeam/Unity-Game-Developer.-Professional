using System;
using UnityEngine;

namespace Otus
{
    //Паттерн компоновщик
    public class ComposableWeapon : WeaponComponent
    {
        public override event Action<IWeapon> OnAttack;

        [SerializeField]
        private Parameters parameters;

        public override bool CanAttack()
        {
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

        public override void Attack()
        {
            this.parameters.trigger.Attack();
        }

        public override void SetActive(bool isActive)
        {
            var weaponComponents = this.parameters.slaveComponents;
            for (int i = 0, count = weaponComponents.Length; i < count; i++)
            {
                var component = weaponComponents[i];
                component.SetActive(isActive);
            }
        }

        private void OnEnable()
        {
            this.parameters.trigger.OnAttack += this.OnTriggerAttack;
        }

        private void OnDisable()
        {
            this.parameters.trigger.OnAttack -= this.OnTriggerAttack;
        }

        private void OnTriggerAttack(IWeapon _)
        {
            var components = this.parameters.slaveComponents;
            for (int i = 0, count = components.Length; i < count; i++)
            {
                var component = components[i];
                component.Attack();
            }
            
            this.OnAttack?.Invoke(this);
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public WeaponComponent trigger;

            [SerializeField]
            public WeaponComponent[] slaveComponents;
        }
    }
}