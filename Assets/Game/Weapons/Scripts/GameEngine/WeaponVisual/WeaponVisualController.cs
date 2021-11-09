using System;
using UnityEngine;

namespace Otus
{
    public sealed class WeaponVisualController : Weapon
    {
        public override event Action<Weapon> OnAttack;

        [SerializeField]
        private GameObject weaponVisual;
        
        protected override void ProcessAttack()
        {
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            this.weaponVisual.SetActive(isActive);
        }
    }
}