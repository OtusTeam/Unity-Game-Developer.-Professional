using System;
using UnityEngine;

namespace Otus
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public abstract event Action<Weapon> OnAttack;

        public abstract void Attack();

        public abstract bool CanAttack();

        public abstract void SetActive(bool isActive);
    }
}