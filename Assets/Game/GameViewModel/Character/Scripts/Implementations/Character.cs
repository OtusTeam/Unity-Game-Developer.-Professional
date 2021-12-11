using System;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.ViewModel
{
    public sealed class Character : ICharacter
    {
        public event Action<int> OnHitPointsChanged
        {
            add { this.hitPointsComponent.OnHitPointsChanged += value; }
            remove { this.hitPointsComponent.OnHitPointsChanged -= value; }
        }

        public event Action<int> OnDamageChanged
        {
            add { this.damageComponent.OnDamageChanged += value; }
            remove { this.damageComponent.OnDamageChanged -= value; }
        }

        public event Action<int> OnMoneyChanged
        {
            add { this.moneyComponent.OnMoneyChanged += value; }
            remove { this.moneyComponent.OnMoneyChanged -= value; }
        }

        public int HitPoints
        {
            get { return this.hitPointsComponent.HitPoints; }
        }

        public int Damage
        {
            get { return this.damageComponent.Damage; }
        }

        public int Money
        {
            get { return this.moneyComponent.Money; }
        }

        public Sprite Icon
        {
            get { return this.unitInfoComponent.Info.portrait; }
        }

        public string Name
        {
            get { return this.unitInfoComponent.Info.name; }
        }

        private readonly HitPointsComponent hitPointsComponent;

        private readonly DamageComponent damageComponent;

        private readonly UnitInfoComponent unitInfoComponent;

        private readonly MoneyStorageComponent moneyComponent;

        public Character(IEntity entity)
        {
            this.hitPointsComponent = entity.GetEntityComponent<HitPointsComponent>();
            this.damageComponent = entity.GetEntityComponent<DamageComponent>();
            this.unitInfoComponent = entity.GetEntityComponent<UnitInfoComponent>();
            this.moneyComponent = entity.GetEntityComponent<MoneyStorageComponent>();
        }
    }
}