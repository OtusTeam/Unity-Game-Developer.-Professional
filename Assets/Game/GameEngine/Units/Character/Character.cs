using System;
using UnityEngine;

namespace Prototype.GameEngine
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

        public int HitPoints
        {
            get { return this.hitPointsComponent.HitPoints; }
        }

        public int Damage
        {
            get { return this.damageComponent.Damage; }
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

        public Character(IEntity entity)
        {
            this.hitPointsComponent = entity.GetEntityComponent<HitPointsComponent>();
            this.damageComponent = entity.GetEntityComponent<DamageComponent>();
            this.unitInfoComponent = entity.GetEntityComponent<UnitInfoComponent>();
        }
    }
}