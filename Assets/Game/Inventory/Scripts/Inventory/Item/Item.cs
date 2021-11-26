using System;
using System.Collections.Generic;
using UnityEngine;

namespace Otus.InventoryModule
{
    [Serializable]
    public sealed class Item
    {
        public ItemType TypeMask
        {
            get { return this.typeMask; }
        }

        [SerializeField]
        private ItemType typeMask;

        [SerializeField]
        private List<IItemComponent> components;

        public Item(params IItemComponent[] components)
        {
            this.Setup(components);
        }

        public void Setup(IEnumerable<IItemComponent> components)
        {
            this.components = new List<IItemComponent>();
            this.typeMask = ItemType.None;

            foreach (var component in components)
            {
                this.components.Add(component);
                this.typeMask |= component.Type;
            }
        }

        public T GetComponent<T>()
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                var component = this.components[i];
                if (component is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Component is not found {typeof(T)}");
        }
    }
}