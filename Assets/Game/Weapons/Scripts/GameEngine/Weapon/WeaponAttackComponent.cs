using System;
using UnityEngine;

namespace Otus
{
    public abstract class WeaponAttackComponent : MonoBehaviour
    {
        public abstract event Action OnAttack;

        [SerializeField]
        private bool isEnable = true;

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
            return true;
        }

        protected abstract void ProcessAttack();
    }
}