using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoneyResourceComponent : EntityComponent 
    {
        public int Money
        {
            get { return this.money; }
        }

        [SerializeField]
        private int money;
    }
}