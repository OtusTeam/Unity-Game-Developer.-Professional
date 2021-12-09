using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoneyResource : IGameResource
    {
        public string Title
        {
            get { return this.info.title; }
        }

        public Sprite Icon
        {
            get { return this.info.icon; }
        }

        public string Value { get; }

        private readonly MoneyResourceInfo info;

        public MoneyResource(MoneyResourceInfo info, int money)
        {
            this.info = info;
            this.Value = money.ToString();
        }
    }
}