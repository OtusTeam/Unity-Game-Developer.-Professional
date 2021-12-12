using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class Castle : ICastle
    {
        public Sprite Icon
        {
            get { return this.unitComponent.Info.portrait; }
        }

        public string Name
        {
            get { return this.unitComponent.Info.name; }
        }

        public int Level
        {
            get { return this.levelComponent.Level; }
        }

        public int Income
        {
            get { return this.incomeComponent.Income; }
        }

        private readonly UnitInfoComponent unitComponent;

        private readonly LevelComponent levelComponent;

        private readonly IncomeComponent incomeComponent;

        public Castle(IEntity entity)
        {
            this.unitComponent = entity.GetEntityComponent<UnitInfoComponent>();
            this.levelComponent = entity.GetEntityComponent<LevelComponent>();
            this.incomeComponent = entity.GetEntityComponent<IncomeComponent>();
        }
    }
}