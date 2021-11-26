using System;
using UnityEngine;

namespace Otus.InventoryModule
{
    [Serializable]
    public class ConsumableItemComponent : IItemComponent
    {
        public event Action OnConsumed;

        public ItemType Type
        {
            get { return this.type; }
        }

        [SerializeField]
        private ItemType type;

        public void Consume()
        {
            Debug.Log("Consumed");
            this.OnConsumed?.Invoke();
        }

        IItemComponent IItemComponent.Clone()
        {
            return new ConsumableItemComponent();
        }
    }
}