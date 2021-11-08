using System;
using UnityEngine;

namespace Otus
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public abstract event Action<Weapon> OnAttack;

        [SerializeField]
        private bool isEnable = true;

        [SerializeField]
        private bool isActive;

        public void Attack()
        {
            if (!this.isEnable)
            {
                return;
            }

            if (this.CanAttack())
            {
                this.ProcessAttack();
            }
        }

        public virtual bool CanAttack()
        {
            return this.isActive;
        }

        public virtual void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }

        protected abstract void ProcessAttack();

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            try
            {
                this.SetActive(this.isActive);
            }
            catch (Exception)
            {
                // ignored
            }
        }
#endif
    }
}