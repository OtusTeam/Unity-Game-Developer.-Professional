using System;

namespace Otus
{
    public sealed class WeaponVisual : Weapon
    {
        public override event Action<Weapon> OnAttack;

        protected override void ProcessAttack()
        {
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            this.gameObject.SetActive(isActive);
        }
    }
}