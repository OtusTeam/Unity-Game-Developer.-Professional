using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoneyStorageComponent : MonoBehaviour
    {
        public event Action<int> OnMoneyChanged;

        public int Money
        {
            get { return this.money; }
        }

        [SerializeField]
        private int money;

        public void AddMoney(int money)
        {
            this.money += money;
            this.OnMoneyChanged?.Invoke(this.money);
        }

        public void SpendMoney(int money)
        {
            this.money -= money;
            this.OnMoneyChanged?.Invoke(this.money);
        }
    }
}