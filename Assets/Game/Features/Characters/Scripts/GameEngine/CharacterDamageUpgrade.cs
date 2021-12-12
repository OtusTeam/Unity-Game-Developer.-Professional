using System;
using Prototype.GameEngine;

namespace Prototype.GameEngine
{
    public sealed class CharacterDamageUpgrade : ICharacterUpgrade
    {
        public int Price { get; } = 100;

        private readonly MoneyStorageComponent moneyStorageComponent;

        private readonly DamageComponent damageComponent;

        public CharacterDamageUpgrade(IEntity entity)
        {
            this.moneyStorageComponent = entity.GetEntityComponent<MoneyStorageComponent>();
            this.damageComponent = entity.GetEntityComponent<DamageComponent>();
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
            this.damageComponent.IncrementDamage(10);
        }
    }
}