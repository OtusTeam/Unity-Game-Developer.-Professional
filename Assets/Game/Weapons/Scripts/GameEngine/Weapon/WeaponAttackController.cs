using System;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponAttackController : MonoBehaviour, IMethodDelegate
    {
        [SerializeField]
        private Parameters parameters;

        [Inject]
        private IDynamicObject weapon;

        object IMethodDelegate.Invoke(object data)
        {
            if (this.CanAttack())
            {
                this.parameters.triggerComponent.Attack();
            }

            return null;
        }

        private bool CanAttack()
        {
            if (!this.parameters.triggerComponent.CanAttack())
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

        private void OnTriggerAttack()
        {
            var weaponComponents = this.parameters.slaveComponents;
            for (int i = 0, count = weaponComponents.Length; i < count; i++)
            {
                var component = weaponComponents[i];
                component.Attack();
            }
        }

        #region Lifecycle

        private void Awake()
        {
            this.weapon.AddMethod(ActionKey.ATTACK, this);
        }

        private void OnEnable()
        {
            this.parameters.triggerComponent.OnAttack += this.OnTriggerAttack;
        }

        private void OnDisable()
        {
            this.parameters.triggerComponent.OnAttack -= this.OnTriggerAttack;
        }

        #endregion

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public WeaponAttackComponent triggerComponent;

            [SerializeField]
            public WeaponAttackComponent[] slaveComponents;
        }
    }
}