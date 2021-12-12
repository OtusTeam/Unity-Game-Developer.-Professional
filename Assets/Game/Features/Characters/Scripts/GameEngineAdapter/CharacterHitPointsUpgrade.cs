using System;
using Prototype.GameEngine;

namespace Prototype.GameEngineAdapter
{
    public sealed class CharacterHitPointsUpgrade : ICharacterUpgrade
    {
        public int Price { get; } = 100;

        private readonly MoneyStorageComponent moneyStorageComponent;

        private readonly HitPointsComponent hitPointsComponent;

        public CharacterHitPointsUpgrade(IEntity entity)
        {
            this.moneyStorageComponent = entity.GetEntityComponent<MoneyStorageComponent>();
            this.hitPointsComponent = entity.GetEntityComponent<HitPointsComponent>();
        }

        public bool CanUpgrade()
        {
            return this.moneyStorageComponent.Money >= this.Price;
        }

        public void Upgrade()
        {
            if (!this.CanUpgrade())
            {
                throw new Exception();
            }

            this.moneyStorageComponent.SpendMoney(this.Price);
            this.hitPointsComponent.IncrementHitPoints(10);
        }
    }
}