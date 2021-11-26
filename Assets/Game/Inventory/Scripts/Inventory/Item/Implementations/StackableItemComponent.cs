using System;
using UnityEngine;

namespace Otus.InventoryModule
{
    [Serializable]
    public sealed class StackableItemComponent : IItemComponent
    {
        public event Action<int> OnStackChanged; 
        
        public int CurrentStack
        {
            get { return this.currentStack; }
        }
        
        public ItemType Type
        {
            get { return this.type; }
        }

        [SerializeField]
        private ItemType type;

        [SerializeField]
        private int currentStack;

        public void SetCurrentStack(int amount)
        {
            this.currentStack = amount;
            this.OnStackChanged?.Invoke(amount);
        }

        IItemComponent IItemComponent.Clone()
        {
            return new StackableItemComponent
            {
                currentStack = this.currentStack,
            };
        }
    }
}