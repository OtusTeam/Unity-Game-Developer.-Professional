using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class DamageComponent : MonoBehaviour
    {
        public event Action<int> OnDamageChanged;

        public int Damage
        {
            get { return this.damage; }
        }

        [SerializeField]
        private int damage;

        public void IncrementDamage(int damage)
        {
            this.damage += damage;
            this.OnDamageChanged?.Invoke(damage);
        }
    }
}