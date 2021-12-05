using System;
using Prototype.UI;

namespace Prototype.GameEngine
{
    public sealed class TradeCharacter : ITradeCharacter
    {
        public event Action<int> OnMoneyChanged
        {
            add { this.moneyComponent.OnMoneyChanged += value; }
            remove { this.moneyComponent.OnMoneyChanged -= value; }
        }

        public event Action<int> OnWoodChanged
        {
            add { this.resourceComponent.OnWoodChanged += value; }
            remove { this.resourceComponent.OnWoodChanged -= value; }
        }

        public event Action<int> OnStoneChanged
        {
            add { this.resourceComponent.OnStoneChanged += value; }
            remove { this.resourceComponent.OnStoneChanged -= value; }
        }

        public int Money
        {
            get { return this.moneyComponent.Money; }
        }

        public int Wood
        {
            get { return this.resourceComponent.Wood; }
        }

        public int Stone
        {
            get { return this.resourceComponent.Stone; }
        }

        private readonly MoneyComponent moneyComponent;

        private readonly ResourceComponent resourceComponent;

        public TradeCharacter(MoneyComponent moneyComponent, ResourceComponent resourceComponent)
        {
            this.moneyComponent = moneyComponent;
            this.resourceComponent = resourceComponent;
        }

        public void SaleWood(ITrader trader)
        {
            var money = trader.WoodPrice * this.resourceComponent.Wood;
            this.moneyComponent.AddMoney(money);
            this.resourceComponent.ResetWood();
        }

        public void SaleStone(ITrader trader)
        {
            var money = trader.StonePrice * this.resourceComponent.Stone;
            this.moneyComponent.AddMoney(money);
            this.resourceComponent.ResetStone();
        }
    }
}